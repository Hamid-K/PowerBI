using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006AA RID: 1706
	public abstract class DbCommandTree
	{
		// Token: 0x06004FF8 RID: 20472 RVA: 0x001215BA File Offset: 0x0011F7BA
		internal DbCommandTree()
		{
			this._useDatabaseNullSemantics = true;
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x001215C9 File Offset: 0x0011F7C9
		internal DbCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, bool useDatabaseNullSemantics = true, bool disableFilterOverProjectionSimplificationForCustomFunctions = false)
		{
			if (!DbCommandTree.IsValidDataSpace(dataSpace))
			{
				throw new ArgumentException(Strings.Cqt_CommandTree_InvalidDataSpace, "dataSpace");
			}
			this._metadata = metadata;
			this._dataSpace = dataSpace;
			this._useDatabaseNullSemantics = useDatabaseNullSemantics;
			this._disableFilterOverProjectionSimplificationForCustomFunctions = disableFilterOverProjectionSimplificationForCustomFunctions;
		}

		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06004FFA RID: 20474 RVA: 0x00121606 File Offset: 0x0011F806
		public bool UseDatabaseNullSemantics
		{
			get
			{
				return this._useDatabaseNullSemantics;
			}
		}

		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06004FFB RID: 20475 RVA: 0x0012160E File Offset: 0x0011F80E
		public bool DisableFilterOverProjectionSimplificationForCustomFunctions
		{
			get
			{
				return this._disableFilterOverProjectionSimplificationForCustomFunctions;
			}
		}

		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06004FFC RID: 20476 RVA: 0x00121616 File Offset: 0x0011F816
		public IEnumerable<KeyValuePair<string, TypeUsage>> Parameters
		{
			get
			{
				return this.GetParameters();
			}
		}

		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06004FFD RID: 20477
		public abstract DbCommandTreeKind CommandTreeKind { get; }

		// Token: 0x06004FFE RID: 20478
		internal abstract IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters();

		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06004FFF RID: 20479 RVA: 0x0012161E File Offset: 0x0011F81E
		public virtual MetadataWorkspace MetadataWorkspace
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06005000 RID: 20480 RVA: 0x00121626 File Offset: 0x0011F826
		public virtual DataSpace DataSpace
		{
			get
			{
				return this._dataSpace;
			}
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x00121630 File Offset: 0x0011F830
		internal void Dump(ExpressionDumper dumper)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("DataSpace", this.DataSpace);
			dumper.Begin(base.GetType().Name, dictionary);
			dumper.Begin("Parameters", null);
			foreach (KeyValuePair<string, TypeUsage> keyValuePair in this.Parameters)
			{
				dumper.Begin("Parameter", new Dictionary<string, object> { { "Name", keyValuePair.Key } });
				dumper.Dump(keyValuePair.Value, "ParameterType");
				dumper.End("Parameter");
			}
			dumper.End("Parameters");
			this.DumpStructure(dumper);
			dumper.End(base.GetType().Name);
		}

		// Token: 0x06005002 RID: 20482
		internal abstract void DumpStructure(ExpressionDumper dumper);

		// Token: 0x06005003 RID: 20483 RVA: 0x00121714 File Offset: 0x0011F914
		public override string ToString()
		{
			return this.Print();
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x0012171C File Offset: 0x0011F91C
		internal string Print()
		{
			return this.PrintTree(new ExpressionPrinter());
		}

		// Token: 0x06005005 RID: 20485
		internal abstract string PrintTree(ExpressionPrinter printer);

		// Token: 0x06005006 RID: 20486 RVA: 0x00121729 File Offset: 0x0011F929
		internal static bool IsValidDataSpace(DataSpace dataSpace)
		{
			return dataSpace == DataSpace.OSpace || DataSpace.CSpace == dataSpace || DataSpace.SSpace == dataSpace;
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x00121738 File Offset: 0x0011F938
		internal static bool IsValidParameterName(string name)
		{
			return !string.IsNullOrWhiteSpace(name) && name.IsValidUndottedName();
		}

		// Token: 0x04001D3C RID: 7484
		private readonly MetadataWorkspace _metadata;

		// Token: 0x04001D3D RID: 7485
		private readonly DataSpace _dataSpace;

		// Token: 0x04001D3E RID: 7486
		private readonly bool _useDatabaseNullSemantics;

		// Token: 0x04001D3F RID: 7487
		private readonly bool _disableFilterOverProjectionSimplificationForCustomFunctions;
	}
}
