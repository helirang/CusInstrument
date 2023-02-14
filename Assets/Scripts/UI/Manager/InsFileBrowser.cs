using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleFileBrowser;
using UnityEngine.Events;

public class InsFileBrowser : MonoBehaviour
{
	public UnityEvent<string> SheetLoad;
	public UnityEvent OnBrowserEvent;
	public UnityEvent OffBrowserEvent;
	string initialPath;
	string fileName = "SheetFolder";

	private void Start()
    {
		initialPath = Application.persistentDataPath;
		FileBrowser.SetFilters(false, new FileBrowser.Filter("Text Files", ".txt"));
		FileBrowser.SetDefaultFilter(".txt");
		FileBrowser.AddQuickLink(fileName, initialPath, null);
		FileBrowser.AddQuickLink("CustomSound", Application.streamingAssetsPath+"/Custom", null);
	}

    public void ShowLoadBrowser()
	{
		OnBrowserEvent.Invoke();
		StartCoroutine(ShowLoadDialogCoroutine());
	}

	public void ShowSaveBrowser(string data)
    {
		OnBrowserEvent.Invoke();
		StartCoroutine(ShowSaveDialogCoroutine(data));
    }

	IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null,
			initialPath, fileName, "Load");

		// Dialog is closed
		// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
		//Debug.Log(FileBrowser.Success);

		if (FileBrowser.Success)
		{
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			//for (int i = 0; i < FileBrowser.Result.Length; i++)
			//	Debug.Log(FileBrowser.Result[i]);

			// Read the bytes of the first file via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);
			string str = System.Text.Encoding.UTF8.GetString(bytes);
			//Debug.Log(str);
			SheetLoad.Invoke(str);

			// Or, copy the first file to persistentDataPath
			//string destinationPath = Path.Combine(initialPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
			//FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
		}
		OffBrowserEvent.Invoke();
	}

	IEnumerator ShowSaveDialogCoroutine(string data)
	{
		yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.FilesAndFolders, false, initialPath, "Sheet", "Save");

		//Debug.Log(FileBrowser.Success);

		if (FileBrowser.Success)
		{
			string path = FileBrowser.Result[0];
			File.WriteAllText(path, data);
		}
		OffBrowserEvent.Invoke();
	}
}
