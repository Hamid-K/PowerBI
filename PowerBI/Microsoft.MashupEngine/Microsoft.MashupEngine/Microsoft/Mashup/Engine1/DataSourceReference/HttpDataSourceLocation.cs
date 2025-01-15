using System;
using System.Collections.Generic;
using System.Net;
using System.Xml;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Web;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018EB RID: 6379
	internal sealed class HttpDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A2A6 RID: 41638 RVA: 0x0021B1C0 File Offset: 0x002193C0
		public HttpDataSourceLocation()
		{
			base.Protocol = "http";
		}

		// Token: 0x17002997 RID: 10647
		// (get) Token: 0x0600A2A7 RID: 41639 RVA: 0x000378A3 File Offset: 0x00035AA3
		public override string ResourceKind
		{
			get
			{
				return "Web";
			}
		}

		// Token: 0x17002998 RID: 10648
		// (get) Token: 0x0600A2A8 RID: 41640 RVA: 0x0021AD62 File Offset: 0x00218F62
		public override string FriendlyName
		{
			get
			{
				return base.GetWebSourceFriendlyName();
			}
		}

		// Token: 0x0600A2A9 RID: 41641 RVA: 0x0021AD6A File Offset: 0x00218F6A
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			return base.TryResolveUri(getHostEntry, out resolvedLocation);
		}

		// Token: 0x0600A2AA RID: 41642 RVA: 0x0021B1D4 File Offset: 0x002193D4
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			Value value = (string.IsNullOrEmpty(optionsJson) ? RecordValue.Empty : JsonModule.Json.Document.Invoke(TextValue.New(optionsJson)));
			RecordValue recordValue = (value.IsNull ? RecordValue.Empty : value.AsRecord);
			bool? flag;
			recordValue = DataSourceLocation.GetAndRemoveLogicalOption(recordValue, "IsWebBrowserContents", out flag);
			bool? flag2;
			recordValue = DataSourceLocation.GetAndRemoveLogicalOption(recordValue, "IsAction", out flag2);
			string text;
			recordValue = DataSourceLocation.GetAndRemoveTextOption(recordValue, "WebMethod", out text);
			IFormulaCreationResult formulaCreationResult;
			try
			{
				bool? flag3 = flag;
				bool flag4 = true;
				if ((flag3.GetValueOrDefault() == flag4) & (flag3 != null))
				{
					recordValue = HttpDataSourceLocation.webBrowserContentsOptionRecord.FromJson(recordValue);
					formulaCreationResult = this.CreateWebBrowserContentsFormula(recordValue);
				}
				else
				{
					flag3 = flag2;
					flag4 = true;
					if ((flag3.GetValueOrDefault() == flag4) & (flag3 != null))
					{
						if (!string.IsNullOrEmpty(text))
						{
							recordValue = WebModule.WebActionRequestOptionRecord.FromJson(recordValue);
							return this.CreateWebFormula("WebAction.Request", recordValue, text);
						}
					}
					else
					{
						if (text == "HEAD")
						{
							recordValue = WebModule.WebHeadersOptionRecord.FromJson(recordValue);
							return this.CreateWebFormula("Web.Headers", recordValue, null);
						}
						if (text == null || text == "GET")
						{
							recordValue = WebModule.WebContentsOptionRecord.FromJson(recordValue);
							return this.CreateWebFormula("Web.Contents", recordValue, null);
						}
					}
					formulaCreationResult = new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, null);
				}
			}
			catch (ArgumentException ex)
			{
				formulaCreationResult = new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return formulaCreationResult;
		}

		// Token: 0x0600A2AB RID: 41643 RVA: 0x0021A118 File Offset: 0x00218318
		public override bool TryGetResource(out IResource resource)
		{
			return base.TryGetUrlResource(out resource);
		}

		// Token: 0x0600A2AC RID: 41644 RVA: 0x0021B344 File Offset: 0x00219544
		private static bool IsWebBrowserContents(string optionsJson)
		{
			if (string.IsNullOrEmpty(optionsJson))
			{
				return false;
			}
			Value value = JsonModule.Json.Document.Invoke(TextValue.New(optionsJson));
			Value value2;
			return value.IsRecord && value.AsRecord.TryGetValue("IsWebBrowserContents", out value2) && value2.IsLogical && value2.AsBoolean;
		}

		// Token: 0x0600A2AD RID: 41645 RVA: 0x0021B398 File Offset: 0x00219598
		private FormulaCreationResult CreateWebBrowserContentsFormula(RecordValue optionsRecord)
		{
			string stringOrNull = base.Address.GetStringOrNull("url");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			if (optionsRecord != null)
			{
				List<NamedValue> list = new List<NamedValue>();
				for (int i = 0; i < optionsRecord.Keys.Length; i++)
				{
					list.Add(new NamedValue(optionsRecord.Keys[i], (optionsRecord.Keys[i] == "WaitFor") ? this.FixUpWaitFor(optionsRecord[i].AsRecord) : optionsRecord[i]));
				}
				if (list.Count > 0)
				{
					return DataSourceLocation.FormatInvocation("Web.BrowserContents", 2, new object[]
					{
						TextValue.New(stringOrNull),
						RecordValue.New(list.ToArray())
					});
				}
			}
			return DataSourceLocation.FormatInvocation("Web.BrowserContents", 1, new object[]
			{
				TextValue.New(stringOrNull),
				optionsRecord
			});
		}

		// Token: 0x0600A2AE RID: 41646 RVA: 0x0021B484 File Offset: 0x00219684
		private FormulaCreationResult CreateWebFormula(string functionName, RecordValue optionsRecord, string method = null)
		{
			string stringOrNull = base.Address.GetStringOrNull("url");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			if (base.Authentication != null && base.Authentication.StartsWith("query-string-key:", StringComparison.Ordinal))
			{
				optionsRecord = optionsRecord ?? RecordValue.Empty;
				optionsRecord = optionsRecord.Concatenate(RecordValue.New(Keys.New("ApiKeyName"), new Value[] { TextValue.New(base.Authentication.Substring("query-string-key:".Length)) })).AsRecord;
			}
			object[] array = new object[(method == null) ? 2 : 3];
			array[0] = TextValue.New(stringOrNull);
			array[1] = optionsRecord;
			if (method != null)
			{
				array[2] = TextValue.New(method);
			}
			FormulaCreationResult formulaCreationResult = DataSourceLocation.FormatInvocation(functionName, 1, array);
			if (formulaCreationResult.Success)
			{
				return DataSourceLocation.CreateContentTypeFormula(base.Address.GetStringOrNull("contentType"), formulaCreationResult);
			}
			return formulaCreationResult;
		}

		// Token: 0x0600A2AF RID: 41647 RVA: 0x0021B568 File Offset: 0x00219768
		private Value FixUpWaitFor(RecordValue waitForRecord)
		{
			List<NamedValue> list = new List<NamedValue>();
			for (int i = 0; i < waitForRecord.Keys.Length; i++)
			{
				list.Add(new NamedValue(waitForRecord.Keys[i], (waitForRecord.Keys[i] == "Timeout") ? DurationValue.New(XmlConvert.ToTimeSpan(waitForRecord[i].AsString)) : waitForRecord[i]));
			}
			return RecordValue.New(list.ToArray());
		}

		// Token: 0x040054CF RID: 21711
		public static readonly DataSourceLocationFactory Factory = new HttpDataSourceLocation.DslFactory();

		// Token: 0x040054D0 RID: 21712
		public const string QueryStringKey = "query-string-key:";

		// Token: 0x040054D1 RID: 21713
		private const string Web_BrowserContents_Function = "Web.BrowserContents";

		// Token: 0x040054D2 RID: 21714
		private static readonly OptionRecordDefinition waitForOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("Selector", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("Timeout", NullableTypeValue.Duration, Value.Null, OptionItemOption.None, null, null)
		});

		// Token: 0x040054D3 RID: 21715
		private static readonly OptionRecordDefinition webBrowserContentsOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("IsWebBrowserContents", NullableTypeValue.Logical, Value.Null, OptionItemOption.ForDsrRoundTripOnly, null, null),
			new OptionItem("ApiKeyName", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
			new OptionItem("WaitFor", HttpDataSourceLocation.waitForOptionRecord.CreateRecordType().Nullable, Value.Null, OptionItemOption.None, delegate(Value option, out object value)
			{
				if (option.IsRecord)
				{
					value = ValueMarshaller.MarshalToClrDictionary(option.AsRecord);
					return true;
				}
				value = null;
				return false;
			}, null)
		});

		// Token: 0x020018EC RID: 6380
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002999 RID: 10649
			// (get) Token: 0x0600A2B1 RID: 41649 RVA: 0x0021B6BE File Offset: 0x002198BE
			public override string Protocol
			{
				get
				{
					return "http";
				}
			}

			// Token: 0x0600A2B2 RID: 41650 RVA: 0x0021B6C5 File Offset: 0x002198C5
			public override IDataSourceLocation New()
			{
				return new HttpDataSourceLocation();
			}

			// Token: 0x0600A2B3 RID: 41651 RVA: 0x0021B6CC File Offset: 0x002198CC
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateUrlLocation("http", resourcePath, out location);
			}
		}
	}
}
