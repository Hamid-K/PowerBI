using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018C4 RID: 6340
	internal sealed class ActiveDirectoryDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A1A7 RID: 41383 RVA: 0x00218B71 File Offset: 0x00216D71
		public ActiveDirectoryDataSourceLocation()
		{
			base.Protocol = "active-directory";
		}

		// Token: 0x17002950 RID: 10576
		// (get) Token: 0x0600A1A8 RID: 41384 RVA: 0x0016D970 File Offset: 0x0016BB70
		public override string ResourceKind
		{
			get
			{
				return "ActiveDirectory";
			}
		}

		// Token: 0x17002951 RID: 10577
		// (get) Token: 0x0600A1A9 RID: 41385 RVA: 0x00218B84 File Offset: 0x00216D84
		public override string FriendlyName
		{
			get
			{
				return base.Address.GetValueOrDefault("domain", base.Protocol) as string;
			}
		}

		// Token: 0x0600A1AA RID: 41386 RVA: 0x00218BA4 File Offset: 0x00216DA4
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			resolvedLocation = base.Clone();
			string text;
			if (base.TryResolveHostName(base.Address.GetStringOrNull("domain"), getHostEntry, out text))
			{
				resolvedLocation.Address["domain"] = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600A1AB RID: 41387 RVA: 0x00218BEC File Offset: 0x00216DEC
		public override bool TryGetResource(out IResource resource)
		{
			string stringOrNull = base.Address.GetStringOrNull("domain");
			if (string.IsNullOrEmpty(stringOrNull))
			{
				resource = null;
				return false;
			}
			resource = Resource.New(this.ResourceKind, stringOrNull);
			return true;
		}

		// Token: 0x0600A1AC RID: 41388 RVA: 0x00218C28 File Offset: 0x00216E28
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			try
			{
				OptionRecordDefinition.Empty.FromJson(optionsJson);
			}
			catch (ArgumentException ex)
			{
				return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.InvalidOption, ex.Message);
			}
			return DataSourceLocation.FormatInvocation("ActiveDirectory.Domains", 1, new object[] { base.Address.GetStringOrNull("domain") });
		}

		// Token: 0x04005496 RID: 21654
		public static readonly DataSourceLocationFactory Factory = new ActiveDirectoryDataSourceLocation.DslFactory();

		// Token: 0x04005497 RID: 21655
		private const string ActiveDirectory_Domains_Function = "ActiveDirectory.Domains";

		// Token: 0x020018C5 RID: 6341
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x17002952 RID: 10578
			// (get) Token: 0x0600A1AE RID: 41390 RVA: 0x00218C98 File Offset: 0x00216E98
			public override string Protocol
			{
				get
				{
					return "active-directory";
				}
			}

			// Token: 0x0600A1AF RID: 41391 RVA: 0x00218C9F File Offset: 0x00216E9F
			public override IDataSourceLocation New()
			{
				return new ActiveDirectoryDataSourceLocation();
			}

			// Token: 0x0600A1B0 RID: 41392 RVA: 0x00218CA6 File Offset: 0x00216EA6
			protected override bool TryCreateFromResourcePath(string resourcePath, out IDataSourceLocation location)
			{
				location = new ActiveDirectoryDataSourceLocation
				{
					Address = new Dictionary<string, object> { { "domain", resourcePath } }
				};
				return true;
			}
		}
	}
}
