<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/183258247/19.1.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T830415)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [Form1.cs](./CS/DateFilter_Customization/Form1.cs) (VB: [Form1.vb](./VB/DateFilter_Customization/Form1.vb))
<!-- default file list end -->

# Dashboard for WinForms - How to Customize the Date Filter Dashboard Item

This example demonstrates how to access the underlying controls to customize the [Date Filter](https://docs.devexpress.com/Dashboard/400675) dashboard item.

It changes the datepicker's caption, background color and paints selected dates and dates contained in the underlying data in a custom manner.

![screenshot](images/screenshot.png)

API in this example:

* [DashboardViewer.DashboardItemControlCreated](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.DashboardItemControlCreated) event
* [DashboardViewer.DashboardItemControlUpdated](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.DashboardItemControlUpdated) event
* [DashboardViewer.DashboardItemBeforeControlDisposed](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.DashboardItemBeforeControlDisposed) event
* [DashboardItemControlEventArgs.DateFilterControl](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardItemControlEventArgs.DateFilterControl) property
* [DateFilterControl](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DateFilterControl) class
* [DateFilterControl.CalendarFrom](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DateFilterControl.CalendarFrom) property
* [DateFilterControl.CalendarTo](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DateFilterControl.CalendarTo) property
* [CalendarControl](https://docs.devexpress.com/WindowsForms/DevExpress.XtraEditors.Controls.CalendarControl) class
* [CalendarControl.CustomDrawDayNumberCell](https://docs.devexpress.com/WindowsForms/DevExpress.XtraEditors.Controls.CalendarControlBase.CustomDrawDayNumberCell) event
* [DateFilterControl.CustomDisplayText](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DateFilterControl.CustomDisplayText) event
* [DashboardViewer.GetItemData](https://docs.devexpress.com/Dashboard/DevExpress.DashboardWin.DashboardViewer.GetItemData(System.String)) method
* [MultiDimensionalData](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.ViewerData.MultiDimensionalData) class
* [MultiDimensionalData.GetMeasures](https://docs.devexpress.com/Dashboard/DevExpress.DashboardCommon.ViewerData.MultiDimensionalData.GetMeasures) method




**See also:**

* [Access to Underlying Controls](https://docs.devexpress.com/Dashboard/18019)

## More Examples
- [How to Create a Dashboard with DateFilterDashboardItem in Code](https://github.com/DevExpress-Examples/winforms-dashboard-create-datefilterdashboarditem)
