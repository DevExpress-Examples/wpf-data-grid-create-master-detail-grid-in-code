Imports DevExpress.Xpf.Grid
Imports System.Windows

Namespace MasterDetailInCode

    Public Partial Class MainWindow
        Inherits Window

        Public Sub New()
            Me.InitializeComponent()
            Dim gridControl As GridControl = CreateGridControl()
            AddDetails(gridControl)
            AddHandler gridControl.Loaded, Sub(d, e) TryCast(d, GridControl)?.ExpandMasterRow(0)
            Me.grid.Children.Add(gridControl)
        End Sub

        Private Function CreateGridControl() As GridControl
            Dim gridControl As GridControl = New GridControl()
            gridControl.AutoGenerateColumns = AutoGenerateColumnsMode.AddNew
            gridControl.ItemsSource = Employees.GetEmployees()
            gridControl.View = New TableView()
            CType(gridControl.View, TableView).AutoWidth = True
            CType(gridControl.View, TableView).ShowGroupPanel = False
            gridControl.View.DetailHeaderContent = NameOf(Employees)
            Return gridControl
        End Function

        Private Sub AddDetails(ByVal gridControl As GridControl)
            Dim detailGridControl As GridControl = New GridControl()
            detailGridControl.AutoGenerateColumns = AutoGenerateColumnsMode.AddNew
            detailGridControl.View = New TableView()
            CType(detailGridControl.View, TableView).AutoWidth = True
            CType(detailGridControl.View, TableView).ShowGroupPanel = False
            detailGridControl.View.DetailHeaderContent = NameOf(Employee.Orders)
            Dim gridDetail As DataControlDetailDescriptor = New DataControlDetailDescriptor()
            gridDetail.ItemsSourcePath = NameOf(Employee.Orders)
            gridDetail.DataControl = detailGridControl
            Dim customDetail As ContentDetailDescriptor = New ContentDetailDescriptor()
            customDetail.ContentTemplate = CType(FindResource("notesTemplate"), DataTemplate)
            customDetail.HeaderContent = NameOf(Employee.Notes)
            Dim chartDetail As ContentDetailDescriptor = New ContentDetailDescriptor()
            chartDetail.ContentTemplate = CType(FindResource("chartTemplate"), DataTemplate)
            chartDetail.HeaderContent = "Stats"
            Dim tabDetail As TabViewDetailDescriptor = New TabViewDetailDescriptor()
            tabDetail.DetailDescriptors.Add(gridDetail)
            tabDetail.DetailDescriptors.Add(customDetail)
            tabDetail.DetailDescriptors.Add(chartDetail)
            gridControl.DetailDescriptor = tabDetail
        End Sub
    End Class
End Namespace
