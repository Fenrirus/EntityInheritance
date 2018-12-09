using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace EntityInheritance
{
    public partial class WebForm1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private DataTable Dane(List<Employees6> employees)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("FirstName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Gender");
            dt.Columns.Add("AnuualSalary");
            dt.Columns.Add("HourlyPay");
            dt.Columns.Add("HoursWorked");
            dt.Columns.Add("Type");
            foreach(Employees6 employee in employees)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = employee.ID;
                dr["FirstName"] = employee.FirstName;
                dr["LastName"] = employee.LastName;
                dr["Gender"] = employee.Gender;

                if (employee is Permament)
                {
                    dr["AnuualSalary"] = ((Permament)employee).AnuualSalary;
                    dr["Type"] = "Permanent";
                }
                else
                {
                    dr["HourlyPay"] = ((Contract)employee).HourlyPay;
                    dr["HoursWorked"] = ((Contract)employee).HoursWorked;
                    dr["Type"] = "Contract";
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            EmployeeDbContext db = new EmployeeDbContext();
            switch (RadioButtonList1.SelectedValue)
            {
                case "Permament":
                    {
                        GridView1.DataSource = db.Employees6.OfType<Permament>().ToList();
                        GridView1.DataBind();
                        break;
                    }
                case "Contract":
                    {
                        GridView1.DataSource = db.Employees6.OfType<Contract>().ToList();
                        GridView1.DataBind();
                        break;
                    }
                default:
                    {
                        GridView1.DataSource = Dane(db.Employees6.ToList());
                        GridView1.DataBind();
                        break;
                    }
            }
        }
    }
}