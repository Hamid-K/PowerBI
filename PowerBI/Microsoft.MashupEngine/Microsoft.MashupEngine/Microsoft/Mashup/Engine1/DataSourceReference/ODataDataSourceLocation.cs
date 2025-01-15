using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.OData;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018F7 RID: 6391
	internal sealed class ODataDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A2E7 RID: 41703 RVA: 0x0021BBD4 File Offset: 0x00219DD4
		public ODataDataSourceLocation()
		{
			base.Protocol = "odata";
		}

		// Token: 0x170029A7 RID: 10663
		// (get) Token: 0x0600A2E8 RID: 41704 RVA: 0x000AF418 File Offset: 0x000AD618
		public override string ResourceKind
		{
			get
			{
				return "OData";
			}
		}

		// Token: 0x170029A8 RID: 10664
		// (get) Token: 0x0600A2E9 RID: 41705 RVA: 0x0021BBE8 File Offset: 0x00219DE8
		public override string FriendlyName
		{
			get
			{
				string text = base.Address.GetStringOrNull("url");
				string stringOrNull = base.Address.GetStringOrNull("resource");
				if (stringOrNull != null)
				{
					text = ODataDataSourceLocation.EndingWithSlash(text) + stringOrNull;
				}
				return text ?? base.Protocol;
			}
		}

		// Token: 0x0600A2EA RID: 41706 RVA: 0x0021AD6A File Offset: 0x00218F6A
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveUri(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A2EB RID: 41707 RVA: 0x0021BC34 File Offset: 0x00219E34
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = ODataModule.OptionRecord.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			try
			{
				string stringOrNull = base.Address.GetStringOrNull("url");
				if (stringOrNull == null)
				{
					return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceLocationUrl, null);
				}
				Uri uri = new Uri(stringOrNull);
				string stringOrNull2 = base.Address.GetStringOrNull("resource");
				if (stringOrNull2 != null)
				{
					Uri uri2 = new Uri(new Uri(ODataDataSourceLocation.EndingWithSlash(stringOrNull)), stringOrNull2);
					if (!uri2.ToString().StartsWith(uri.ToString(), StringComparison.OrdinalIgnoreCase))
					{
						return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceLocationUrl, null);
					}
					uri = uri2;
				}
				string text = uri.ToString();
				if (base.Authentication != null && base.Authentication.StartsWith("query-string-key:", StringComparison.Ordinal))
				{
					recordValue = recordValue ?? RecordValue.Empty;
					recordValue = recordValue.Concatenate(RecordValue.New(Keys.New("ApiKeyName"), new Value[] { TextValue.New(base.Authentication.Substring("query-string-key:".Length)) })).AsRecord;
					return DataSourceLocation.FormatInvocation("OData.Feed", 1, new object[]
					{
						TextValue.New(text),
						Value.Null,
						recordValue
					});
				}
				return DataSourceLocation.FormatInvocation("OData.Feed", 1, new object[]
				{
					TextValue.New(text),
					Value.Null,
					recordValue
				});
			}
			catch (UriFormatException)
			{
			}
			return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceLocationUrl, null);
		}

		// Token: 0x0600A2EC RID: 41708 RVA: 0x0021BDE4 File Offset: 0x00219FE4
		public override bool TryGetResource(out IResource resource)
		{
			string stringOrNull = base.Address.GetStringOrNull("resource");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				return base.TryGetUrlResource(out resource);
			}
			string stringOrNull2 = base.Address.GetStringOrNull("url");
			if (string.IsNullOrEmpty(stringOrNull2))
			{
				resource = null;
				return false;
			}
			resource = Resource.New(this.ResourceKind, ODataDataSourceLocation.EndingWithSlash(stringOrNull2) + stringOrNull);
			return true;
		}

		// Token: 0x0600A2ED RID: 41709 RVA: 0x0021BE4A File Offset: 0x0021A04A
		private static string EndingWithSlash(string url)
		{
			return url.TrimEnd(new char[] { '/' }) + "/";
		}

		// Token: 0x040054E0 RID: 21728
		public static readonly DataSourceLocationFactory Factory = new ODataDataSourceLocation.DslFactory();

		// Token: 0x040054E1 RID: 21729
		private const string OData_Feed_Function = "OData.Feed";

		// Token: 0x020018F8 RID: 6392
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029A9 RID: 10665
			// (get) Token: 0x0600A2EF RID: 41711 RVA: 0x0021BE73 File Offset: 0x0021A073
			public override string Protocol
			{
				get
				{
					return "odata";
				}
			}

			// Token: 0x0600A2F0 RID: 41712 RVA: 0x0021BE7A File Offset: 0x0021A07A
			public override IDataSourceLocation New()
			{
				return new ODataDataSourceLocation();
			}

			// Token: 0x0600A2F1 RID: 41713 RVA: 0x0021BE84 File Offset: 0x0021A084
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				int num = resourcePath.IndexOf(".svc/", StringComparison.Ordinal);
				location = new ODataDataSourceLocation();
				if (num != -1)
				{
					string text = resourcePath.Substring(0, num + ".svc/".Length - 1);
					if (text.Length != resourcePath.Length)
					{
						string text2 = resourcePath.Substring(text.Length + 1, resourcePath.Length - text.Length - 1);
						if (this.AtMostOne(text2, '/'))
						{
							location.Address["url"] = text;
							location.Address["resource"] = text2.TrimEnd(new char[] { '/' });
							return true;
						}
					}
				}
				location.Address["url"] = resourcePath;
				return true;
			}

			// Token: 0x0600A2F2 RID: 41714 RVA: 0x0021BF44 File Offset: 0x0021A144
			private bool AtMostOne(string str, char match)
			{
				bool flag = false;
				for (int i = 0; i < str.Length; i++)
				{
					if (str[i] == match)
					{
						if (flag)
						{
							return false;
						}
						flag = true;
					}
				}
				return true;
			}
		}
	}
}
