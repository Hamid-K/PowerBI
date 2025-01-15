using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020000BC RID: 188
	[CLSCompliant(false)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	internal sealed class TupleElementNamesAttribute : Attribute
	{
		// Token: 0x0600061B RID: 1563 RVA: 0x0001881C File Offset: 0x00016A1C
		public TupleElementNamesAttribute(string[] transformNames)
		{
			if (transformNames == null)
			{
				throw new ArgumentNullException("transformNames");
			}
			this._transformNames = transformNames;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600061C RID: 1564 RVA: 0x0001883C File Offset: 0x00016A3C
		public IList<string> TransformNames
		{
			get
			{
				return this._transformNames;
			}
		}

		// Token: 0x040001D8 RID: 472
		private readonly string[] _transformNames;
	}
}
