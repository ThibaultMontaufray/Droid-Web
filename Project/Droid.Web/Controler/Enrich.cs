namespace Droid.Web
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Enrich
    {
        #region Attribute
        private char[] _charUpperList = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        #endregion

        #region Properties
        #endregion

        #region Constructor
        public Enrich()
        {

        }
        #endregion

        #region Methods public
        public void SaveObject(object obj)
        {
            //List<string> list = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Select(field => field.Name).ToList();
            //string columnsFormat = string.Empty;
            //string values = string.Empty;
            //string update = string.Empty;
            //string currentVal = string.Empty;
            //string currentProp = string.Empty;

            //for (int i = 0; i < list.Count; i++)
            //{
            //    if (list[i].StartsWith("_") || list[i].StartsWith("On") || list[i].Contains("EnrichmentDone"))
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        PropertyInfo propertyInfo = obj.GetType().GetProperty(list[i].Split('>')[0].Split('<')[1]);
            //        if (propertyInfo.GetValue(obj, null) == null) continue;
            //        currentVal =  propertyInfo.GetValue(obj, null).ToString();
            //        currentProp = AttributToColumnName(propertyInfo.Name);

            //        if (!string.IsNullOrEmpty(columnsFormat)) columnsFormat += ", ";
            //        if (!string.IsNullOrEmpty(values)) values += ", ";
            //        if (!string.IsNullOrEmpty(update)) update += ", ";

            //        columnsFormat += currentProp;
            //        values += "\"" + currentVal + "\"";
            //        update += currentProp + "=\"" + currentVal + "\"";
            //    }
            //}

            //Droid.database.MySqlAdapter.ExecuteQuery(string.Format("insert into t_{0} ({1}) values ({2})  on duplicate key update {3}", obj.GetType().ToString().Split('.')[obj.GetType().ToString().Split('.').Length - 1], columnsFormat, values, update));
        }
        #endregion

        #region Methods protected
        protected string AttributToColumnName(string attributName)
        {
            string columnName = attributName.ToUpper();
            foreach (string item in attributName.Split(_charUpperList))
            {
                if (!string.IsNullOrEmpty(item)) columnName = columnName.Replace(item.ToUpper(), item + "_");
            }
            if (columnName.EndsWith("_")) { columnName = columnName.Substring(0, columnName.Length - 1); }
            return columnName;
        }
        protected string ColumnNameToAttribut(string columnName)
        {
            string attributName = string.Empty;
            foreach (string item in columnName.Split('_'))
            {
                attributName += item.Substring(0, 1).ToUpper() + item.Substring(1, item.Length - 1);
            }
            return attributName;
        }
        #endregion
    }
}
