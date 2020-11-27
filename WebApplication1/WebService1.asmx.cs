using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.OleDb;

namespace WebApplication1
{

	[WebService]
	public class WebService1 : WebService
	{
		[WebMethod]
		public string SearchStudent(string fam)
		{
            string stId;
            string stMark1;
            string stMark2;
 

            using (OleDbConnection connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.12.0;Data Source=Students.mdb;"))
            {
                connection.Open();

                try
                {
                    string query1 = string.Format("SELECT [Код студента] FROM Студенты WHERE [ФИО] = '{0}'", fam);
                    OleDbCommand command1 = new OleDbCommand(query1, connection);
                    stId = command1.ExecuteScalar().ToString();
                }
                catch (NullReferenceException)
                {
                    connection.Close();
                    return $"Студента {fam} нет в списках";
                }

                    string query2 = $"SELECT [Оценка 1] FROM Оценки WHERE [Код студента] = {stId}";
                    OleDbCommand command2 = new OleDbCommand(query2, connection);
                    stMark1 = command2.ExecuteScalar().ToString();

                    string query3 = $"SELECT [Оценка 2] FROM Оценки WHERE [Код студента] = {stId}";
                    OleDbCommand command3 = new OleDbCommand(query3, connection);
                    stMark2 = command3.ExecuteScalar().ToString();
 
                if (stMark1 == "5" && stMark2 == "5")
                {

                    return $"Студент {fam} является отличником";
                }
                else if ((stMark1 == "4" || stMark2 == "4") && (stMark1 == "5" || stMark2 == "5"))
                {
                    return $"Студент {fam} является ударником";
                }
                else if(stMark1 == "3" || stMark2 == "3")
                {
                    return $"Студент {fam} является троечником";
                }
                else
                {
                    return $"Студент {fam} является двоечником";
                }
            }
        }
	}	
}



