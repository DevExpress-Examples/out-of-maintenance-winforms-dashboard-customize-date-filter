using DevExpress.DashboardCommon.ViewerData;
using DevExpress.DashboardWin;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace DateFilter_Customization
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        DateTime minValue;
        DateTime maxValue;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            dashboardViewer1.DashboardItemControlCreated += DashboardViewer1_DashboardItemControlCreated;
            dashboardViewer1.DashboardItemControlUpdated += DashboardViewer1_DashboardItemControlUpdated;
            dashboardViewer1.DashboardItemBeforeControlDisposed += DashboardViewer1_DashboardItemBeforeControlDisposed;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dashboardViewer1.LoadDashboard("Data\\datefilter-sample-dashboard.xml");
        }


        private void DashboardViewer1_DashboardItemControlCreated(object sender, DevExpress.DashboardWin.DashboardItemControlEventArgs e)
        {
            DashboardViewer designer = sender as DashboardViewer;

            if (e.DateFilterControl != null)
            {
                SubscribeDateFilterControlEvents(e.DateFilterControl);
                e.DateFilterControl.BackColor = Color.AliceBlue;
            }
        }

        private void SubscribeDateFilterControlEvents(DateFilterControl dateFilter)
        {
            dateFilter.CalendarFrom.CustomDrawDayNumberCell += Calendar_CustomDrawDayNumberCell;
            dateFilter.CalendarTo.CustomDrawDayNumberCell += Calendar_CustomDrawDayNumberCell;
            dateFilter.CustomDisplayText += DateFilter_CustomDisplayText;
        }

        private void DateFilter_CustomDisplayText(object sender, CustomDisplayTextEventArgs e)
        {
            e.DisplayText = (e.Value is DateTime) ? string.Format("{0:d}", e.Value) : "Click for the Date Picker";
        }

        private void Calendar_CustomDrawDayNumberCell(object sender, DevExpress.XtraEditors.Calendar.CustomDrawDayNumberCellEventArgs e)
        {
            CalendarControl calendar = sender as CalendarControl;

            if (e.Date > minValue && e.Date < maxValue)
                e.Style.BackColor = Color.FromArgb(80, 0, 100, 10);


            if (e.Selected && e.View == DateEditCalendarViewType.MonthInfo)
            {
                StringFormat dayFormat = new StringFormat();
                dayFormat.Alignment = StringAlignment.Center;
                dayFormat.LineAlignment = StringAlignment.Center;
                Rectangle rect = e.ContentBounds;
                rect.Inflate(2, 2);
                Font cellFont = new Font(calendar.CalendarAppearance.DayCell.Font.FontFamily, calendar.CalendarAppearance.DayCell.Font.Size + 2);
                e.Cache.FillRectangle(new SolidBrush(Color.Yellow), e.ContentBounds);
                e.Cache.Graphics.DrawString($"{e.Date.Day}", cellFont, Brushes.Black, rect, dayFormat);
                e.Handled = true;
            }
        }

        private void DashboardViewer1_DashboardItemControlUpdated(object sender, DashboardItemControlEventArgs e)
        {
            if (e.DateFilterControl != null)
            {
                UpdateDateFilterControl(e.DateFilterControl);
            }
        }

        private void UpdateDateFilterControl(DateFilterControl dateFilter)
        {
            MultiDimensionalData mddata = dashboardViewer1.GetItemData("dateFilterDashboardItem1");
            IList<DateTime> values = mddata.GetMeasures().Select(measure => mddata.GetValue(measure).Value).OfType<DateTime>().ToList();
            minValue = values.Min();
            maxValue = values.Max();
        }

        private void UnsubscribeDateFilterControlEvents(DateFilterControl dateFilter)
        {
            CalendarControl calendarTo = dateFilter.CalendarTo;
            CalendarControl calendarFrom = dateFilter.CalendarFrom;
            calendarFrom.CustomDrawDayNumberCell -= Calendar_CustomDrawDayNumberCell;
            calendarTo.CustomDrawDayNumberCell -= Calendar_CustomDrawDayNumberCell;
        }

        private void DashboardViewer1_DashboardItemBeforeControlDisposed(object sender, DashboardItemControlEventArgs e)
        {
            if (e.DateFilterControl != null)
            {
                UnsubscribeDateFilterControlEvents(e.DateFilterControl);
            }
        }
    }
}
