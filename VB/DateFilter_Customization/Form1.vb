Imports DevExpress.DashboardCommon.ViewerData
Imports DevExpress.DashboardWin
Imports DevExpress.XtraEditors.Controls
Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Drawing
Imports System.Linq

Namespace DateFilter_Customization
	Partial Public Class Form1
		Inherits DevExpress.XtraEditors.XtraForm

		Private minValue As Date
		Private maxValue As Date
		Public Sub New()
			InitializeComponent()
			AddHandler Me.Load, AddressOf Form1_Load
			AddHandler dashboardViewer1.DashboardItemControlCreated, AddressOf DashboardViewer1_DashboardItemControlCreated
			AddHandler dashboardViewer1.DashboardItemControlUpdated, AddressOf DashboardViewer1_DashboardItemControlUpdated
			AddHandler dashboardViewer1.DashboardItemBeforeControlDisposed, AddressOf DashboardViewer1_DashboardItemBeforeControlDisposed
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
			dashboardViewer1.LoadDashboard("Data\datefilter-sample-dashboard.xml")
		End Sub


		Private Sub DashboardViewer1_DashboardItemControlCreated(ByVal sender As Object, ByVal e As DevExpress.DashboardWin.DashboardItemControlEventArgs)
			If e.DateFilterControl IsNot Nothing Then
				SubscribeDateFilterControlEvents(e.DateFilterControl)
				e.DateFilterControl.BackColor = Color.AliceBlue
			End If
		End Sub

		Private Sub SubscribeDateFilterControlEvents(ByVal dateFilter As DateFilterControl)
			AddHandler dateFilter.CalendarFrom.CustomDrawDayNumberCell, AddressOf Calendar_CustomDrawDayNumberCell
			AddHandler dateFilter.CalendarTo.CustomDrawDayNumberCell, AddressOf Calendar_CustomDrawDayNumberCell
			AddHandler dateFilter.CustomDisplayText, AddressOf DateFilter_CustomDisplayText
		End Sub

		Private Sub DateFilter_CustomDisplayText(ByVal sender As Object, ByVal e As CustomDisplayTextEventArgs)
			e.DisplayText = If(TypeOf e.Value Is Date, String.Format("{0:d}", e.Value), "Click for the Date Picker")
		End Sub

		Private Sub Calendar_CustomDrawDayNumberCell(ByVal sender As Object, ByVal e As DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs)
			Dim calendar As CalendarControl = TryCast(sender, CalendarControl)

			If e.Date > minValue AndAlso e.Date < maxValue Then
				e.Style.BackColor = Color.FromArgb(80, 0, 100, 10)
			End If


			If e.Selected AndAlso e.View = DateEditCalendarViewType.MonthInfo Then
				Dim dayFormat As New StringFormat()
				dayFormat.Alignment = StringAlignment.Center
				dayFormat.LineAlignment = StringAlignment.Center
				Dim rect As Rectangle = e.ContentBounds
				rect.Inflate(2, 2)
				Dim cellFont As New Font(calendar.CalendarAppearance.DayCell.Font.FontFamily, calendar.CalendarAppearance.DayCell.Font.Size + 2)
				e.Cache.FillRectangle(New SolidBrush(Color.Yellow), e.ContentBounds)
				e.Cache.Graphics.DrawString($"{e.Date.Day}", cellFont, Brushes.Black, rect, dayFormat)
				e.Handled = True
			End If
		End Sub

		Private Sub DashboardViewer1_DashboardItemControlUpdated(ByVal sender As Object, ByVal e As DashboardItemControlEventArgs)
			If e.DateFilterControl IsNot Nothing Then
				UpdateDateFilterControl(e.DateFilterControl)
			End If
		End Sub

		Private Sub UpdateDateFilterControl(ByVal dateFilter As DateFilterControl)
			Dim mddata As MultiDimensionalData = dashboardViewer1.GetItemData("dateFilterDashboardItem1")
			Dim values As IList(Of Date) = mddata.GetMeasures().Select(Function(measure) mddata.GetValue(measure).Value).OfType(Of Date)().ToList()
			minValue = values.Min()
			maxValue = values.Max()
		End Sub

		Private Sub UnsubscribeDateFilterControlEvents(ByVal dateFilter As DateFilterControl)
			Dim calendarTo As CalendarControl = dateFilter.CalendarTo
			Dim calendarFrom As CalendarControl = dateFilter.CalendarFrom
			RemoveHandler calendarFrom.CustomDrawDayNumberCell, AddressOf Calendar_CustomDrawDayNumberCell
			RemoveHandler calendarTo.CustomDrawDayNumberCell, AddressOf Calendar_CustomDrawDayNumberCell
		End Sub

		Private Sub DashboardViewer1_DashboardItemBeforeControlDisposed(ByVal sender As Object, ByVal e As DashboardItemControlEventArgs)
			If e.DateFilterControl IsNot Nothing Then
				UnsubscribeDateFilterControlEvents(e.DateFilterControl)
			End If
		End Sub
	End Class
End Namespace
