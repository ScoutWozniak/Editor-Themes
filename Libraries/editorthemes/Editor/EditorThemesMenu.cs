using Editor;



public static class EditorThemesMenu
{
	[Menu( "Editor", "Editor Themes/Editor" )]
	public static void OpenMyMenu()
	{
		var windowExample = new ThemeEditorWidget( null );
		windowExample.Show();

	}
}
