using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.AzureBlobs;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018CC RID: 6348
	internal class AzureBlobsDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A1DD RID: 41437 RVA: 0x0021953B File Offset: 0x0021773B
		public AzureBlobsDataSourceLocation()
		{
			base.Protocol = "azure-blobs";
		}

		// Token: 0x17002962 RID: 10594
		// (get) Token: 0x0600A1DE RID: 41438 RVA: 0x0021954E File Offset: 0x0021774E
		protected virtual string ContentsFunction
		{
			get
			{
				return "AzureStorage.BlobContents";
			}
		}

		// Token: 0x17002963 RID: 10595
		// (get) Token: 0x0600A1DF RID: 41439 RVA: 0x00219555 File Offset: 0x00217755
		protected virtual string ContainersFunction
		{
			get
			{
				return "AzureStorage.Blobs";
			}
		}

		// Token: 0x17002964 RID: 10596
		// (get) Token: 0x0600A1E0 RID: 41440 RVA: 0x0015BE05 File Offset: 0x0015A005
		public override string ResourceKind
		{
			get
			{
				return "AzureBlobs";
			}
		}

		// Token: 0x17002965 RID: 10597
		// (get) Token: 0x0600A1E1 RID: 41441 RVA: 0x0021955C File Offset: 0x0021775C
		public override string FriendlyName
		{
			get
			{
				string stringOrNull = base.Address.GetStringOrNull("url");
				if (stringOrNull != null)
				{
					return stringOrNull;
				}
				string stringOrNull2 = base.Address.GetStringOrNull("account");
				if (stringOrNull2 == null)
				{
					return base.Protocol;
				}
				string stringOrNull3 = base.Address.GetStringOrNull("container");
				StringBuilder stringBuilder = new StringBuilder(string.IsNullOrEmpty(stringOrNull3) ? this.GetAzureBlobsAccountUrl(stringOrNull2, base.Address.GetStringOrNull("domain")) : this.GetAzureBlobsContainerPath(stringOrNull2, base.Address.GetStringOrNull("domain"), stringOrNull3));
				if (stringOrNull3 != null)
				{
					string stringOrNull4 = base.Address.GetStringOrNull("prefix");
					if (stringOrNull4 != null)
					{
						stringBuilder.Append(stringOrNull4);
					}
					string stringOrNull5 = base.Address.GetStringOrNull("name");
					if (stringOrNull5 != null)
					{
						stringBuilder.Append(stringOrNull5);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x0600A1E2 RID: 41442 RVA: 0x00219634 File Offset: 0x00217834
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			IFormulaCreationResult formulaCreationResult;
			if (this.TryCreateFormulaWithUrl(optionsJson, out formulaCreationResult))
			{
				return formulaCreationResult;
			}
			return this.CreateFormulaWithAccount(optionsJson);
		}

		// Token: 0x0600A1E3 RID: 41443 RVA: 0x00219658 File Offset: 0x00217858
		protected bool TryCreateFormulaWithUrl(string optionsJson, out IFormulaCreationResult result)
		{
			string stringOrNull = base.Address.GetStringOrNull("url");
			if (stringOrNull != null)
			{
				RecordValue recordValue;
				try
				{
					recordValue = AzureBlobsModule.SupportedOptions.FromJson(optionsJson);
				}
				catch (ArgumentException ex)
				{
					result = new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
					return true;
				}
				result = DataSourceLocation.FormatInvocation(this.ContentsFunction, 1, new object[]
				{
					TextValue.New(stringOrNull),
					recordValue
				});
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x0600A1E4 RID: 41444 RVA: 0x002196D4 File Offset: 0x002178D4
		protected IFormulaCreationResult CreateFormulaWithAccount(string optionsJson)
		{
			RecordValue recordValue = null;
			try
			{
				recordValue = AzureBlobsModule.SupportedOptions.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			string stringOrNull = base.Address.GetStringOrNull("account");
			if (stringOrNull == null)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			string stringOrNull2 = base.Address.GetStringOrNull("domain");
			string text = ((stringOrNull2 != null) ? this.GetAzureBlobsAccountUrl(stringOrNull, stringOrNull2) : stringOrNull);
			string container = base.Address.GetStringOrNull("container");
			string stringOrNull3 = base.Address.GetStringOrNull("prefix");
			string name = base.Address.GetStringOrNull("name");
			string stringOrNull4 = base.Address.GetStringOrNull("contentType");
			ExpressionBuilder b = ExpressionBuilder.Instance;
			IExpression expression = b.Invoke(this.ContainersFunction, 1, new object[] { text, recordValue });
			if (container == null && stringOrNull3 == null && name == null && stringOrNull4 == null)
			{
				return new FormulaCreationResult(expression);
			}
			if (string.IsNullOrEmpty(container))
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
			}
			Func<IExpression, IExpression> func = (IExpression src) => b.Navigate(src, "Name", container, "Data");
			if (stringOrNull3 == null && name == null && stringOrNull4 == null)
			{
				return new FormulaCreationResult(func(expression));
			}
			string folderPath = this.GetAzureBlobsContainerPath(stringOrNull, stringOrNull2, container);
			if (stringOrNull3 != null)
			{
				if (name != null || stringOrNull4 != null)
				{
					return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
				}
				return new FormulaCreationResult(b.Let(new VariableInitializer[]
				{
					b.Declare("Source", expression, true),
					b.Declare("Container", func(b.Identifier("Source")), true),
					b.Declare("Folder", b.Invoke("Table.SelectRows", new object[]
					{
						b.Identifier("Container"),
						b.Each(b.Invoke("Text.StartsWith", new object[]
						{
							b.Member(Identifier.Underscore, "Name"),
							stringOrNull3
						}))
					}), true)
				}));
			}
			else
			{
				if (string.IsNullOrEmpty(name))
				{
					return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidDataSourceReference, null);
				}
				Func<IExpression, IExpression> func2 = (IExpression src) => b.Navigate(src, "Folder Path", folderPath, "Name", name, "Content");
				if (stringOrNull4 == null)
				{
					return new FormulaCreationResult(func2(func(expression)));
				}
				List<VariableInitializer> list = new List<VariableInitializer>();
				list.Add(b.Declare("Source", expression, true));
				list.Add(b.Declare("Container", func(b.Identifier("Source")), true));
				list.Add(b.Declare("Blob", func2(b.Identifier("Container")), true));
				if (stringOrNull4 == "csv")
				{
					list.Add(b.Declare("Content", b.Invoke("Csv.Document", new object[] { b.Identifier("Blob") }), true));
					list.Add(b.Declare("PromoteHeaders", b.Invoke("Table.PromoteHeaders", new object[] { b.Identifier("Content") }), true));
				}
				else
				{
					FormulaCreationResult formulaCreationResult = DataSourceLocation.CreateContentTypeFormula(stringOrNull4, new FormulaCreationResult(b.Identifier("Blob")));
					if (!formulaCreationResult.Success)
					{
						return formulaCreationResult;
					}
					list.Add(b.Declare("Content", formulaCreationResult.FormulaExpression, true));
				}
				return new FormulaCreationResult(b.Let(list.ToArray()));
			}
			IFormulaCreationResult formulaCreationResult2;
			return formulaCreationResult2;
		}

		// Token: 0x0600A1E5 RID: 41445 RVA: 0x00219B00 File Offset: 0x00217D00
		public string GetAzureBlobsContainerPath(string account, string domain, string containerName)
		{
			return DataSourceLocation.GetAzureContainerPath(account, domain ?? "blob.core.windows.net", containerName);
		}

		// Token: 0x0600A1E6 RID: 41446 RVA: 0x00219B13 File Offset: 0x00217D13
		public string GetAzureBlobsAccountUrl(string account, string domain = null)
		{
			return DataSourceLocation.GetAzureAccountUrl(account, domain ?? "blob.core.windows.net", null).Uri.AbsoluteUri;
		}

		// Token: 0x0600A1E7 RID: 41447 RVA: 0x00219B30 File Offset: 0x00217D30
		public override bool TryGetResource(out IResource resource)
		{
			string stringOrNull = base.Address.GetStringOrNull("url");
			if (stringOrNull != null)
			{
				resource = Resource.New(this.ResourceKind, stringOrNull);
				return true;
			}
			return base.TryGetAzureResource("blob.core.windows.net", out resource);
		}

		// Token: 0x040054A0 RID: 21664
		public static readonly DataSourceLocationFactory Factory = new AzureBlobsDataSourceLocation.DslFactory();

		// Token: 0x040054A1 RID: 21665
		private const string DefaultDomain = "blob.core.windows.net";

		// Token: 0x040054A2 RID: 21666
		private const string AzureStorage_Blobs_Function = "AzureStorage.Blobs";

		// Token: 0x040054A3 RID: 21667
		private const string AzureStorage_BlobContents_Function = "AzureStorage.BlobContents";

		// Token: 0x040054A4 RID: 21668
		private const string Csv_Document_Function = "Csv.Document";

		// Token: 0x040054A5 RID: 21669
		private const string Table_PromoteHeaders_Function = "Table.PromoteHeaders";

		// Token: 0x020018CD RID: 6349
		protected class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002966 RID: 10598
			// (get) Token: 0x0600A1E9 RID: 41449 RVA: 0x00219B79 File Offset: 0x00217D79
			public override string Protocol
			{
				get
				{
					return "azure-blobs";
				}
			}

			// Token: 0x0600A1EA RID: 41450 RVA: 0x00219B80 File Offset: 0x00217D80
			public override IDataSourceLocation New()
			{
				return new AzureBlobsDataSourceLocation();
			}

			// Token: 0x0600A1EB RID: 41451 RVA: 0x00219B87 File Offset: 0x00217D87
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				return this.TryCreateWithUrl(resourcePath, out location) || this.TryCreateWithAccount(resourcePath, out location);
			}

			// Token: 0x0600A1EC RID: 41452 RVA: 0x00219BA0 File Offset: 0x00217DA0
			protected bool TryCreateWithUrl(string resourcePath, out IDataSourceLocation location)
			{
				Uri uri;
				if (Uri.TryCreate(resourcePath, UriKind.Absolute, out uri) && uri.Segments.Length >= 3 && !uri.Segments.Last<string>().EndsWith("/", StringComparison.Ordinal))
				{
					location = DataSourceLocationFactory.New(this.Protocol);
					location.Address["url"] = uri.AbsoluteUri;
					return true;
				}
				location = null;
				return false;
			}

			// Token: 0x0600A1ED RID: 41453 RVA: 0x00219C05 File Offset: 0x00217E05
			protected bool TryCreateWithAccount(string resourcePath, out IDataSourceLocation location)
			{
				return DataSourceLocationFactory.TryCreateAzureLocation(this.Protocol, resourcePath, out location);
			}
		}
	}
}
