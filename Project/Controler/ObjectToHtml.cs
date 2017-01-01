namespace Droid_web
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public static class ObjectToHtml
    {
        #region Attribute
        private static string _img;
        private static string _title;
        private static StringBuilder _html;
        private static Type _type;

        #region HTML TEMPLATE
        private const string HTMLCSS = @"    <style type='text/css'>
	          .tableoth {{ 
	            margin:auto;
		        font-family:arial,sans-serif-light,sans-serif;
		        margin: 20px;
		        color: black; 
		        border:1px solid black;
                background-color:#000;
                border : 1px solid #373737;
	          }}
	          .tableoth td {{
		        padding:1px;
		        font-size:12;
		        color:#eee;
                text-align:top;
                float: top;
	          }}
	          .tableoth td span
	          {{
		        color:#FC0;
	          }}
	          #name{{
		        color:#FFF;
                background-color:#900;
		        font:arial,helvetica,sans-serif;
		        font-size:16px;
		        font-style:bold;
		        text-align:left;
                height:25px;
                padding:5px;
                padding-left:10px;
	          }}
            </style>      
            <meta http-equiv='X-UA-Compatible' content='IE=10' />";
        private const string HTMLPICTURETITLE = @"	<table border=0 cellspacing=0 class='tableoth'>
		        <tr>
			        <td style='vertical-align: top;' rowspan=100><img style='max-width:150px;' src='{1}' />
			        <td id='name'>{0}</td>
		        </tr>
                {2}
	        </table>
        ";
        private const string HTMLTITLE = @"	<table border=0 cellspacing=0 class='tableoth'>
		        <tr>
			        <td id='name'>{0}</td>
		        </tr>
                {1}
	        </table>
        ";
        private const string HTMLDEFAULT = @"	<table border=0 cellspacing=0 class='tableoth'>
                {0}
	        </table>
        ";
        #endregion
        #endregion

        #region Properties
        #endregion

        #region Methods public
        public static string GetHtml(object obj)
        {
            _title = string.Empty;
            _img = string.Empty;
            _type = obj.GetType();
            _html = new StringBuilder();
            foreach (var item in _type.GetProperties())
            {
                if (item.Name.ToLower().Equals("dump") || item.Name.ToLower().Equals("enrichmentdone") || _type.GetProperty(item.Name).GetValue(obj, null) == null)
                {
                    continue;
                }
                else if (item.Name.ToLower().Contains("image") || item.Name.ToLower().Contains("photo"))
                {
                    _img = _type.GetProperty(item.Name).GetValue(obj, null).ToString();
                }
                else if (item.Name.ToLower().Equals("titre") || item.Name.ToLower().Equals("nom"))
                {
                    _title = _type.GetProperty(item.Name).GetValue(obj, null).ToString();
                }
                else if (item.PropertyType.Name.StartsWith("List"))
                {
                    _html.AppendLine("<tr><td><ul>");
                    foreach (var objLst in (IEnumerable)_type.GetProperty(item.Name).GetValue(obj, null))
                    {
                        string resultLst = string.Empty;
                        foreach (var ppt in objLst.GetType().GetProperties())
                        {
                            if (!string.IsNullOrEmpty(resultLst)) resultLst += " ";
                            resultLst += objLst.GetType().GetProperty(ppt.Name).GetValue(objLst, null);
                        }
                        _html.AppendLine(string.Format("<li>{0}</li>", resultLst));
                    }
                    _html.AppendLine("</ul></td></tr>");
                }
                else if (item.Name.ToLower().Equals("lien"))
                {
                    _html.AppendLine(string.Format("<tr><td> {0} : <span><a href=\"{1}\">{1}</a></span></td></tr>", item.Name, _type.GetProperty(item.Name).GetValue(obj, null)));
                }
                else if (item.Name.ToLower().Equals("images") && item.PropertyType.Name.Equals("List<String>"))
                {
                    _html.AppendLine(string.Format("<tr><td><img src=\"{0}\" /></td></tr>", _type.GetProperty(item.Name).GetValue(obj, null)));
                }
                else if (item.PropertyType.Name.Equals("String") && item.Name.StartsWith("Frame"))
                {
                    _html.AppendLine(string.Format("<tr><td><span><iframe width=\"800\" height=\"600\" frameborder=\"0\" style=\"border:0\" src=\"{0}\"allowfullscreen></iframe></span></td></tr>", _type.GetProperty(item.Name).GetValue(obj, null)));
                }
                else if (_type.GetProperty(item.Name).GetValue(obj, null) != null &&
                    !string.IsNullOrEmpty(_type.GetProperty(item.Name).GetValue(obj, null).ToString()) &&
                    ((item.PropertyType.Name.Equals("String") ||
                    item.PropertyType.Name.Equals("DateTime") ||
                    item.PropertyType.Name.Equals("Int32") ||
                    item.PropertyType.Name.Equals("TimeSpan") ||
                    item.PropertyType.Name.Equals("Double"))
                    ))
                {
                    _html.AppendLine(string.Format("<tr><td> {0} : <span>{1}</span></td></tr>", item.Name, _type.GetProperty(item.Name).GetValue(obj, null)));
                }
            }
            if (!string.IsNullOrEmpty(_title) && !string.IsNullOrEmpty(_img))
            {
                return string.Format(HTMLCSS + HTMLPICTURETITLE, _title, _img, _html.ToString());
            }
            else if (!string.IsNullOrEmpty(_title))
            {
                return string.Format(HTMLCSS + HTMLTITLE, _title, _html.ToString());
            }
            else
            {
                return string.Format(HTMLCSS + HTMLDEFAULT, _html.ToString());
            }
        }
        #endregion

        #region Methods private
        #endregion
    }
}
