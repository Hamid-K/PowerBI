using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000010 RID: 16
	[CLSCompliant(false)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public sealed class TupleElementNamesAttribute : Attribute
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x000080C5 File Offset: 0x000062C5
		public TupleElementNamesAttribute(string[] transformNames)
		{
			if (transformNames == null)
			{
				throw new ArgumentNullException("transformNames");
			}
			this._transformNames = transformNames;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000080E2 File Offset: 0x000062E2
		public IList<string> TransformNames
		{
			get
			{
				return this._transformNames;
			}
		}

		// Token: 0x0400004C RID: 76
		private readonly string[] _transformNames;
	}
}
