using System;

namespace System.Diagnostics
{
	// Token: 0x02000028 RID: 40
	internal sealed class DiagNode<T>
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00005C60 File Offset: 0x00003E60
		public DiagNode(T value)
		{
			this.Value = value;
		}

		// Token: 0x04000080 RID: 128
		public T Value;

		// Token: 0x04000081 RID: 129
		public DiagNode<T> Next;
	}
}
