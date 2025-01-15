using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000536 RID: 1334
	public abstract class FunctionImportMapping : MappingItem
	{
		// Token: 0x060041A7 RID: 16807 RVA: 0x000DD918 File Offset: 0x000DBB18
		internal FunctionImportMapping(EdmFunction functionImport, EdmFunction targetFunction)
		{
			this._functionImport = functionImport;
			this._targetFunction = targetFunction;
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x000DD92E File Offset: 0x000DBB2E
		public EdmFunction FunctionImport
		{
			get
			{
				return this._functionImport;
			}
		}

		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060041A9 RID: 16809 RVA: 0x000DD936 File Offset: 0x000DBB36
		public EdmFunction TargetFunction
		{
			get
			{
				return this._targetFunction;
			}
		}

		// Token: 0x040016C5 RID: 5829
		private readonly EdmFunction _functionImport;

		// Token: 0x040016C6 RID: 5830
		private readonly EdmFunction _targetFunction;
	}
}
