using System;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Dracula
{
	// Token: 0x0200042A RID: 1066
	public sealed class BagMultiCountTableBuilder : IMultiCountTableBuilder
	{
		// Token: 0x06001624 RID: 5668 RVA: 0x000812EC File Offset: 0x0007F4EC
		public BagMultiCountTableBuilder(IHostEnvironment env, SubComponent<ICountTableBuilder, SignatureCountTableBuilder> defaultBuilderArgs, int labelCardinality)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("BagMultiCountTableBuilder");
			this._builder = ComponentCatalog.CreateInstance<ICountTableBuilder, SignatureCountTableBuilder>(defaultBuilderArgs, new object[] { this._host, labelCardinality });
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00081341 File Offset: 0x0007F541
		public void IncrementOne(int iCol, uint key, uint labelKey, double value)
		{
			this.IncrementSlot(iCol, 0, key, labelKey, value);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00081350 File Offset: 0x0007F550
		public void IncrementSlot(int iCol, int iSlot, uint key, uint labelKey, double value)
		{
			uint num = Hashing.MurmurRound((uint)iCol, (uint)iSlot);
			uint num2 = Hashing.MurmurRound(num, key);
			this._builder.Increment((long)((ulong)num2), (long)((ulong)labelKey), value);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00081380 File Offset: 0x0007F580
		public IMultiCountTable CreateMultiCountTable()
		{
			return new SharedMultiCountTable(this._host, this._builder.CreateCountTable());
		}

		// Token: 0x04000D99 RID: 3481
		public const string LoaderSignature = "BagMultiCountTableBuilder";

		// Token: 0x04000D9A RID: 3482
		private readonly IHost _host;

		// Token: 0x04000D9B RID: 3483
		private readonly ICountTableBuilder _builder;
	}
}
