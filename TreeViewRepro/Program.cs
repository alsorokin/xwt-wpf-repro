using System;
using Xwt;

class XwtDemo
{
    static int count;
    [STAThread]
    static void Main()
    {
        Application.Initialize(ToolkitType.Wpf);
        var f = new DataField<string>();
        TreeStore ts = new TreeStore(f);

        var tree = new TreeView(ts);
        tree.Columns.Add("MyColumn", f);
        tree.HeadersVisible = false;
        tree.MinHeight = 300;

        Notebook nb = new Notebook();
        nb.Add(new Label("Click the button below to add elements to the TreeView on the second tab. \nObserve that the result is different if you click it before changing tabs and after."), "One");
        var vb = new VBox();
        vb.PackStart(nb);

        var addButton = new Button("Add element");
        addButton.Clicked += (o, e) =>
        {
            var navigator = ts.AddNode().SetValue(f, "New element " + (++count).ToString());
            tree.SelectRow(navigator.CurrentPosition);
        };

        var addChildButton = new Button("Add child element");
        addChildButton.Clicked += (o, e) =>
        {
            var navigator = ts.GetNavigatorAt(tree.SelectedRow);
            navigator.AddChild().SetValue(f, "New child");
            tree.SelectRow(navigator.CurrentPosition);
        };

        vb.PackEnd(addChildButton);
        vb.PackEnd(addButton);

        nb.Add(tree, "Two");
        var mainWindow = new Window()
        {
            Title = "Xwt TreeView Repro",
            Width = 500,
            Height = 500
        };
        mainWindow.Content = vb;
        mainWindow.CloseRequested += (o, e) =>
        {
            Xwt.Application.Exit();
        };
        mainWindow.Show();
        Application.Run();
        mainWindow.Dispose();
    }
}
