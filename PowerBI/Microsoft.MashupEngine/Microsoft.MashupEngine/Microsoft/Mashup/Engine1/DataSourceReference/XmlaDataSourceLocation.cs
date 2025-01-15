using System;
using System.Net;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.Library;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x02001915 RID: 6421
	internal sealed class XmlaDataSourceLocation : DataSourceLocation
	{
		// Token: 0x0600A3B4 RID: 41908 RVA: 0x0021DCD0 File Offset: 0x0021BED0
		public XmlaDataSourceLocation()
		{
			base.Protocol = "xmla";
		}

		// Token: 0x170029E3 RID: 10723
		// (get) Token: 0x0600A3B5 RID: 41909 RVA: 0x0021929A File Offset: 0x0021749A
		public override string FriendlyName
		{
			get
			{
				return base.GetRelationalSourceFriendlyName();
			}
		}

		// Token: 0x0600A3B6 RID: 41910 RVA: 0x0021DCE4 File Offset: 0x0021BEE4
		public override bool TryResolve(Func<string, IPHostEntry> getHostEntry, out IDataSourceLocation resolvedLocation)
		{
			resolvedLocation = base.Clone();
			string[] array = base.Address.GetStringOrNull("server").Split(new char[] { ':' });
			string text;
			if (array.Length <= 2 && base.TryResolveHostName(array[0].Trim(), getHostEntry, out text))
			{
				if (array.Length == 2 && array[1] != "2383")
				{
					text = text + ":" + array[1].Trim();
				}
				resolvedLocation.Address["server"] = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600A3B7 RID: 41911 RVA: 0x0021DC9B File Offset: 0x0021BE9B
		public override IFormulaCreationResult CreateFormula(string optionsJson)
		{
			return new FormulaCreationResult(DataSourceReferenceReaderFailureReason.UnrecognizedDataSourceType, null);
		}

		// Token: 0x0600A3B8 RID: 41912 RVA: 0x000E6755 File Offset: 0x000E4955
		public override bool TryGetResource(out IResource resource)
		{
			resource = null;
			return false;
		}

		// Token: 0x0400550F RID: 21775
		public static readonly DataSourceLocationFactory Factory = new XmlaDataSourceLocation.DslFactory();

		// Token: 0x02001916 RID: 6422
		private sealed class DslFactory : DataSourceLocationFactory
		{
			// Token: 0x170029E4 RID: 10724
			// (get) Token: 0x0600A3BA RID: 41914 RVA: 0x0021DD7D File Offset: 0x0021BF7D
			public override string Protocol
			{
				get
				{
					return "xmla";
				}
			}

			// Token: 0x0600A3BB RID: 41915 RVA: 0x0021DD84 File Offset: 0x0021BF84
			public override IDataSourceLocation New()
			{
				return new XmlaDataSourceLocation();
			}
		}
	}
}
