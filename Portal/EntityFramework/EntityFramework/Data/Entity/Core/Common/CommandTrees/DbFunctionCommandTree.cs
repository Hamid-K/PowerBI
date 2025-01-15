using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x020006BD RID: 1725
	public sealed class DbFunctionCommandTree : DbCommandTree
	{
		// Token: 0x060050BC RID: 20668 RVA: 0x00121E00 File Offset: 0x00120000
		public DbFunctionCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, EdmFunction edmFunction, TypeUsage resultType, IEnumerable<KeyValuePair<string, TypeUsage>> parameters)
			: base(metadata, dataSpace, true, false)
		{
			Check.NotNull<EdmFunction>(edmFunction, "edmFunction");
			this._edmFunction = edmFunction;
			this._resultType = resultType;
			List<string> list = new List<string>();
			List<TypeUsage> list2 = new List<TypeUsage>();
			if (parameters != null)
			{
				foreach (KeyValuePair<string, TypeUsage> keyValuePair in parameters)
				{
					list.Add(keyValuePair.Key);
					list2.Add(keyValuePair.Value);
				}
			}
			this._parameterNames = new ReadOnlyCollection<string>(list);
			this._parameterTypes = new ReadOnlyCollection<TypeUsage>(list2);
		}

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x00121EAC File Offset: 0x001200AC
		public EdmFunction EdmFunction
		{
			get
			{
				return this._edmFunction;
			}
		}

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x060050BE RID: 20670 RVA: 0x00121EB4 File Offset: 0x001200B4
		public TypeUsage ResultType
		{
			get
			{
				return this._resultType;
			}
		}

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x060050BF RID: 20671 RVA: 0x00121EBC File Offset: 0x001200BC
		public override DbCommandTreeKind CommandTreeKind
		{
			get
			{
				return DbCommandTreeKind.Function;
			}
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x00121EBF File Offset: 0x001200BF
		internal override IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters()
		{
			int num;
			for (int idx = 0; idx < this._parameterNames.Count; idx = num + 1)
			{
				yield return new KeyValuePair<string, TypeUsage>(this._parameterNames[idx], this._parameterTypes[idx]);
				num = idx;
			}
			yield break;
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x00121ECF File Offset: 0x001200CF
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			if (this.EdmFunction != null)
			{
				dumper.Dump(this.EdmFunction);
			}
		}

		// Token: 0x060050C2 RID: 20674 RVA: 0x00121EE5 File Offset: 0x001200E5
		internal override string PrintTree(ExpressionPrinter printer)
		{
			return printer.Print(this);
		}

		// Token: 0x04001D90 RID: 7568
		private readonly EdmFunction _edmFunction;

		// Token: 0x04001D91 RID: 7569
		private readonly TypeUsage _resultType;

		// Token: 0x04001D92 RID: 7570
		private readonly ReadOnlyCollection<string> _parameterNames;

		// Token: 0x04001D93 RID: 7571
		private readonly ReadOnlyCollection<TypeUsage> _parameterTypes;
	}
}
