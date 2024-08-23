#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CRUDmvc.Views.CrudOperation
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[System.CodeDom.Compiler.GeneratedCodeAttribute("RazorTemplatePreprocessor", "2.6.0.0")]
public partial class UserIndex : UserIndexBase
{

#line hidden

#line 1 "UserIndex.cshtml"
public CRUDmvc.Model.ReadRecordResponse Model { get; set; }

#line default
#line hidden


public override void Execute()
{
WriteLiteral("<!DOCTYPE html>\n<html");

WriteLiteral(" lang=\"en\"");

WriteLiteral(">\n<head>\n    <meta");

WriteLiteral(" charset=\"UTF-8\"");

WriteLiteral(">\n    <meta");

WriteLiteral(" name=\"viewport\"");

WriteLiteral(" content=\"width=device-width, initial-scale=1.0\"");

WriteLiteral(">\n    <title>Read Records</title>\n    <link");

WriteLiteral(" href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css\"");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" integrity=\"sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEw" +
"IH\"");

WriteLiteral(" crossorigin=\"anonymous\"");

WriteLiteral(">\n    <link");

WriteLiteral(" href=\"https://cdn.datatables.net/1.13.4/css/dataTables.bootstrap5.min.css\"");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(@">
    <style>
        table, th, td {
            margin: 1px;
        }

        .btnStyle {
            margin: 0 auto;
            display: block;
        }

        .bdr {
            border-radius: 6px;
            overflow: hidden;
        }
    </style>
</head>
<body");

WriteLiteral(" style=\"background-color: black; color:white;\"");

WriteLiteral(">\n    <center><h1");

WriteLiteral(" style=\"color: white; text-shadow: 2px 2px 5px red;\"");

WriteLiteral(">Purdue Directory</h1></center>\n    <div");

WriteLiteral(" class=\"d-flex flex-column align-items-center\"");

WriteLiteral(">\n        <div");

WriteLiteral(" class=\"w-75\"");

WriteLiteral(">\n            <table");

WriteLiteral(" class=\"table table-striped table-hover bdr\"");

WriteLiteral(" id=\"myTable\"");

WriteLiteral(" style=\"background-color: beige; color: black;\"");

WriteLiteral(">\n                <thead>\n                    <tr>\n                        <th>Fi" +
"rst Name</th>\n                        <th>Last Name</th>\n                       " +
" <th> GPA </th>\n                        <th");

WriteLiteral(" class=\"no_sort\"");

WriteLiteral(">Update</th>\n                    </tr>\n                </thead>\n                <" +
"tbody");

WriteLiteral(" id=\"mainTable\"");

WriteLiteral(" class=\"rounded-2\"");

WriteLiteral(">\n");


#line 40 "UserIndex.cshtml"
                    

#line default
#line hidden

#line 40 "UserIndex.cshtml"
                     foreach (var record in Model.data)
                    {


#line default
#line hidden
WriteLiteral("                        <tr");

WriteAttribute ("id", " id=\"", "\""

#line 42 "UserIndex.cshtml"
, Tuple.Create<string,object,bool> ("", record.Id

#line default
#line hidden
, false)
);
WriteLiteral(">\n                            <td");

WriteLiteral(" class=\"fName\"");

WriteLiteral(" contenteditable=\"true\"");

WriteLiteral(">");


#line 43 "UserIndex.cshtml"
                                                                Write(record.firstName);


#line default
#line hidden
WriteLiteral("</td>\n                            <td");

WriteLiteral(" class=\"lName\"");

WriteLiteral(" contenteditable=\"true\"");

WriteLiteral(">");


#line 44 "UserIndex.cshtml"
                                                                Write(record.lastName);


#line default
#line hidden
WriteLiteral("</td>\n                            <td");

WriteLiteral(" class=\"GPA\"");

WriteLiteral(" contenteditable=\"true\"");

WriteLiteral(">");


#line 45 "UserIndex.cshtml"
                                                              Write(record.GPA);


#line default
#line hidden
WriteLiteral("</td>\n                            <td><button");

WriteLiteral(" type=\"button\"");

WriteLiteral(" class=\"btn btn-outline-primary\"");

WriteAttribute ("onclick", " onclick=\"", "\""
, Tuple.Create<string,object,bool> ("", "updateRecord(\'", true)

#line 46 "UserIndex.cshtml"
                                                                      , Tuple.Create<string,object,bool> ("", record.Id

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", "\')", true)
);
WriteLiteral(">Update</button></td>\n                        </tr>\n");


#line 48 "UserIndex.cshtml"
                    }


#line default
#line hidden
WriteLiteral("                </tbody>\n            </table>\n        </div>\n    </div>\n\n    <!--" +
" Include jQuery before DataTables and Bootstrap JS -->\n    <script");

WriteLiteral(" src=\"https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.0/jquery.min.js\"");

WriteLiteral("></script>\n    <script");

WriteLiteral(" src=\"https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.j" +
"s\"");

WriteLiteral(" integrity=\"sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIe" +
"Hz\"");

WriteLiteral(" crossorigin=\"anonymous\"");

WriteLiteral("></script>\n    <script");

WriteLiteral(" src=\"https://cdn.datatables.net/1.13.4/js/jquery.dataTables.min.js\"");

WriteLiteral("></script>\n    <script");

WriteLiteral(" src=\"https://cdn.datatables.net/1.13.4/js/dataTables.bootstrap5.min.js\"");

WriteLiteral(@"></script>

    <script>function updateRecord(id) {
            var element = document.getElementById(id);
            var listTd = element.getElementsByTagName('td');
            var firstName = listTd[0].outerText;
            var lastName = listTd[1].outerText;
            var GPA = listTd[2].outerText;
            var data = {
                YOURId: id,
                firstName: firstName,
                lastName: lastName,
                GPA: GPA
            };
            $.ajax({
                url: '/en/CrudOperation/UpdateGPA',
                type: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    console.log('Record updated successfully');
                    location.reload();
                },
                error: function (xhr, status, error) {
                    console.log('Error updating record: ' + error);
                }
            });
        }</script>
</body>
</html>
");

}
}

// NOTE: this is the default generated helper class. You may choose to extract it to a separate file 
// in order to customize it or share it between multiple templates, and specify the template's base 
// class via the @inherits directive.
public abstract class UserIndexBase
{

		// This field is OPTIONAL, but used by the default implementation of Generate, Write, WriteAttribute and WriteLiteral
		//
		System.IO.TextWriter __razor_writer;

		// This method is OPTIONAL
		//
		/// <summary>Executes the template and returns the output as a string.</summary>
		/// <returns>The template output.</returns>
		public string GenerateString ()
		{
			using (var sw = new System.IO.StringWriter ()) {
				Generate (sw);
				return sw.ToString ();
			}
		}

		// This method is OPTIONAL, you may choose to implement Write and WriteLiteral without use of __razor_writer
		// and provide another means of invoking Execute.
		//
		/// <summary>Executes the template, writing to the provided text writer.</summary>
		/// <param name="writer">The TextWriter to which to write the template output.</param>
		public void Generate (System.IO.TextWriter writer)
		{
			__razor_writer = writer;
			Execute ();
			__razor_writer = null;
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>Writes a literal value to the template output without HTML escaping it.</summary>
		/// <param name="value">The literal value.</param>
		protected void WriteLiteral (string value)
		{
			__razor_writer.Write (value);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>Writes a literal value to the TextWriter without HTML escaping it.</summary>
		/// <param name="writer">The TextWriter to which to write the literal.</param>
		/// <param name="value">The literal value.</param>
		protected static void WriteLiteralTo (System.IO.TextWriter writer, string value)
		{
			writer.Write (value);
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>Writes a value to the template output, HTML escaping it if necessary.</summary>
		/// <param name="value">The value.</param>
		/// <remarks>The value may be a Action<System.IO.TextWriter>, as returned by Razor helpers.</remarks>
		protected void Write (object value)
		{
			WriteTo (__razor_writer, value);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>Writes an object value to the TextWriter, HTML escaping it if necessary.</summary>
		/// <param name="writer">The TextWriter to which to write the value.</param>
		/// <param name="value">The value.</param>
		/// <remarks>The value may be a Action<System.IO.TextWriter>, as returned by Razor helpers.</remarks>
		protected static void WriteTo (System.IO.TextWriter writer, object value)
		{
			if (value == null)
				return;

			var write = value as Action<System.IO.TextWriter>;
			if (write != null) {
				write (writer);
				return;
			}

			//NOTE: a more sophisticated implementation would write safe and pre-escaped values directly to the
			//instead of double-escaping. See System.Web.IHtmlString in ASP.NET 4.0 for an example of this.
			writer.Write(System.Net.WebUtility.HtmlEncode (value.ToString ()));
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>
		/// Conditionally writes an attribute to the template output.
		/// </summary>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="prefix">The prefix of the attribute.</param>
		/// <param name="suffix">The suffix of the attribute.</param>
		/// <param name="values">Attribute values, each specifying a prefix, value and whether it's a literal.</param>
		protected void WriteAttribute (string name, string prefix, string suffix, params Tuple<string,object,bool>[] values)
		{
			WriteAttributeTo (__razor_writer, name, prefix, suffix, values);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>
		/// Conditionally writes an attribute to a TextWriter.
		/// </summary>
		/// <param name="writer">The TextWriter to which to write the attribute.</param>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="prefix">The prefix of the attribute.</param>
		/// <param name="suffix">The suffix of the attribute.</param>
		/// <param name="values">Attribute values, each specifying a prefix, value and whether it's a literal.</param>
		///<remarks>Used by Razor helpers to write attributes.</remarks>
		protected static void WriteAttributeTo (System.IO.TextWriter writer, string name, string prefix, string suffix, params Tuple<string,object,bool>[] values)
		{
			// this is based on System.Web.WebPages.WebPageExecutingBase
			// Copyright (c) Microsoft Open Technologies, Inc.
			// Licensed under the Apache License, Version 2.0
			if (values.Length == 0) {
				// Explicitly empty attribute, so write the prefix and suffix
				writer.Write (prefix);
				writer.Write (suffix);
				return;
			}

			bool first = true;
			bool wroteSomething = false;

			for (int i = 0; i < values.Length; i++) {
				Tuple<string,object,bool> attrVal = values [i];
				string attPrefix = attrVal.Item1;
				object value = attrVal.Item2;
				bool isLiteral = attrVal.Item3;

				if (value == null) {
					// Nothing to write
					continue;
				}

				// The special cases here are that the value we're writing might already be a string, or that the 
				// value might be a bool. If the value is the bool 'true' we want to write the attribute name instead
				// of the string 'true'. If the value is the bool 'false' we don't want to write anything.
				//
				// Otherwise the value is another object (perhaps an IHtmlString), and we'll ask it to format itself.
				string stringValue;
				bool? boolValue = value as bool?;
				if (boolValue == true) {
					stringValue = name;
				} else if (boolValue == false) {
					continue;
				} else {
					stringValue = value as string;
				}

				if (first) {
					writer.Write (prefix);
					first = false;
				} else {
					writer.Write (attPrefix);
				}

				if (isLiteral) {
					writer.Write (stringValue ?? value);
				} else {
					WriteTo (writer, stringValue ?? value);
				}
				wroteSomething = true;
			}
			if (wroteSomething) {
				writer.Write (suffix);
			}
		}
		// This method is REQUIRED. The generated Razor subclass will override it with the generated code.
		//
		///<summary>Executes the template, writing output to the Write and WriteLiteral methods.</summary>.
		///<remarks>Not intended to be called directly. Call the Generate method instead.</remarks>
		public abstract void Execute ();

}
}
#pragma warning restore 1591
