using DevExpress.Xpf.Grid;
using System.Windows;

namespace MasterDetailInCode {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            GridControl gridControl = CreateGridControl();
            AddDetails(gridControl);
            gridControl.Loaded += (d, e) => { (d as GridControl)?.ExpandMasterRow(0); };
            grid.Children.Add(gridControl);
        }

        private GridControl CreateGridControl() {
            GridControl gridControl = new GridControl();
            gridControl.AutoGenerateColumns = AutoGenerateColumnsMode.AddNew;
            gridControl.ItemsSource = Employees.GetEmployees();
            gridControl.View = new TableView();
            ((TableView)gridControl.View).AutoWidth = true;
            ((TableView)gridControl.View).ShowGroupPanel = false;
            gridControl.View.DetailHeaderContent = nameof(Employees);

            return gridControl;
        }
        private void AddDetails(GridControl gridControl) {
            GridControl detailGridControl = new GridControl();
            detailGridControl.AutoGenerateColumns = AutoGenerateColumnsMode.AddNew;
            detailGridControl.View = new TableView();
            ((TableView)detailGridControl.View).AutoWidth = true;
            ((TableView)detailGridControl.View).ShowGroupPanel = false;
            detailGridControl.View.DetailHeaderContent = nameof(Employee.Orders);

            DataControlDetailDescriptor gridDetail = new DataControlDetailDescriptor();
            gridDetail.ItemsSourcePath = nameof(Employee.Orders);
            gridDetail.DataControl = detailGridControl;

            ContentDetailDescriptor customDetail = new ContentDetailDescriptor();
            customDetail.ContentTemplate = (DataTemplate)FindResource("notesTemplate");
            customDetail.HeaderContent = nameof(Employee.Notes);

            ContentDetailDescriptor chartDetail = new ContentDetailDescriptor();
            chartDetail.ContentTemplate = (DataTemplate)FindResource("chartTemplate");
            chartDetail.HeaderContent = "Stats";

            TabViewDetailDescriptor tabDetail = new TabViewDetailDescriptor();
            tabDetail.DetailDescriptors.Add(gridDetail);
            tabDetail.DetailDescriptors.Add(customDetail);
            tabDetail.DetailDescriptors.Add(chartDetail);

            gridControl.DetailDescriptor = tabDetail;
        }
    }
}
