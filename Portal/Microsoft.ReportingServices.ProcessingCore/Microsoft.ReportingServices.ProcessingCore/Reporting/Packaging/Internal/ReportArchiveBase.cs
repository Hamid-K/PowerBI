using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Microsoft.Reporting.Packaging.Internal
{
	// Token: 0x02000003 RID: 3
	internal abstract class ReportArchiveBase
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00005E9F File Offset: 0x0000409F
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x00005EA7 File Offset: 0x000040A7
		protected Dictionary<string, string> MustUnderstandNamespaces
		{
			get
			{
				return this.m_mustUnderstandNamespaces;
			}
			set
			{
				this.m_mustUnderstandNamespaces = value;
			}
		}

		// Token: 0x060002D9 RID: 729
		internal abstract Stream GetReportDefinitionStream();

		// Token: 0x060002DA RID: 730
		protected abstract Stream GetApplicationPropertiesStream();

		// Token: 0x060002DB RID: 731
		protected abstract void HandleApplicationPropertiesError(ReportArchiveBase.ApplicationPropertiesError error, params string[] items);

		// Token: 0x060002DC RID: 732 RVA: 0x00005EB0 File Offset: 0x000040B0
		protected void VerifyApplicationProperties()
		{
			using (Stream applicationPropertiesStream = this.GetApplicationPropertiesStream())
			{
				if (applicationPropertiesStream != null)
				{
					XElement root = XDocument.Load(new StreamReader(applicationPropertiesStream)).Root;
					if (root.Name.LocalName != "Properties" || root.GetDefaultNamespace().NamespaceName != "http://schemas.microsoft.com/sqlserver/reporting/2012/01/extendedproperties")
					{
						this.HandleApplicationPropertiesError(ReportArchiveBase.ApplicationPropertiesError.InvalidAppPropsRootElement, Array.Empty<string>());
					}
					XNamespace xnamespace = "http://schemas.openxmlformats.org/markup-compatibility/2006";
					XAttribute xattribute = root.Attribute(xnamespace + "MustUnderstand");
					if (xattribute != null && xattribute.Value != string.Empty)
					{
						string[] array = xattribute.Value.Split(new char[] { ' ' });
						if (array.Length != 0)
						{
							List<string> list = new List<string>();
							List<string> list2 = new List<string>();
							foreach (string text in array)
							{
								XNamespace namespaceOfPrefix = root.GetNamespaceOfPrefix(text);
								if (namespaceOfPrefix == null)
								{
									list.Add(text);
								}
								else if (!this.m_mustUnderstandNamespaces.ContainsKey(namespaceOfPrefix.NamespaceName))
								{
									list2.Add(namespaceOfPrefix.NamespaceName);
								}
							}
							if (list.Count > 0)
							{
								this.HandleApplicationPropertiesError(ReportArchiveBase.ApplicationPropertiesError.UndefinedMustUnderstandNamespaces, new string[] { ReportArchiveBase.ConvertToString(list) });
							}
							if (list2.Count > 0)
							{
								this.HandleApplicationPropertiesError(ReportArchiveBase.ApplicationPropertiesError.UnrecognizedNonIgnorableNamespaces, new string[] { ReportArchiveBase.ConvertToString(list2) });
							}
						}
					}
				}
			}
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00006044 File Offset: 0x00004244
		private static string ConvertToString(List<string> items)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in items)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("'");
				stringBuilder.Append(text);
				stringBuilder.Append("'");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000060CC File Offset: 0x000042CC
		protected static void CopyStream(Stream inputStream, Stream outputStream)
		{
			byte[] array = new byte[4096];
			for (int i = inputStream.Read(array, 0, array.Length); i > 0; i = inputStream.Read(array, 0, array.Length))
			{
				outputStream.Write(array, 0, i);
			}
		}

		// Token: 0x04000003 RID: 3
		protected const string AppPropsType = "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportpackage/relationships/extendedproperties";

		// Token: 0x04000004 RID: 4
		protected const string ReportDefinitionType = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportpackage/relationships/reportdefinition";

		// Token: 0x04000005 RID: 5
		protected const string ReportStateType = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportpackage/relationships/reportstate";

		// Token: 0x04000006 RID: 6
		protected const string ReportSectionPreviewType = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportpackage/relationships/sectionpreviewimage";

		// Token: 0x04000007 RID: 7
		protected const string GeocodingCacheType = "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportpackage/relationships/geocodingcache";

		// Token: 0x04000008 RID: 8
		protected const string ReportImageResourceType = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportpackage/relationships/image";

		// Token: 0x04000009 RID: 9
		protected const string NsMarkupCompatibility = "http://schemas.openxmlformats.org/markup-compatibility/2006";

		// Token: 0x0400000A RID: 10
		protected const string NsExtendedProperties = "http://schemas.microsoft.com/sqlserver/reporting/2012/01/extendedproperties";

		// Token: 0x0400000B RID: 11
		protected const string ElementNameProperties = "Properties";

		// Token: 0x0400000C RID: 12
		protected const string AttributeNameMustUnderstand = "MustUnderstand";

		// Token: 0x0400000D RID: 13
		private Dictionary<string, string> m_mustUnderstandNamespaces = new Dictionary<string, string>
		{
			{ "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition", "rdl2010" },
			{ "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition", "rdl2011" },
			{ "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportdefinition", "rdl2012" },
			{ "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportdefinition", "rdl2013" },
			{ "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportstate", "rdlrs2011" },
			{ "http://schemas.microsoft.com/sqlserver/reporting/2012/01/reportstate", "rdlrs2012" },
			{ "http://schemas.microsoft.com/sqlserver/reporting/2013/01/reportstate", "rdlrs2013" }
		};

		// Token: 0x02000907 RID: 2311
		protected enum ApplicationPropertiesError
		{
			// Token: 0x04003EA9 RID: 16041
			InvalidAppPropsRootElement,
			// Token: 0x04003EAA RID: 16042
			UndefinedMustUnderstandNamespaces,
			// Token: 0x04003EAB RID: 16043
			UnrecognizedNonIgnorableNamespaces
		}
	}
}
