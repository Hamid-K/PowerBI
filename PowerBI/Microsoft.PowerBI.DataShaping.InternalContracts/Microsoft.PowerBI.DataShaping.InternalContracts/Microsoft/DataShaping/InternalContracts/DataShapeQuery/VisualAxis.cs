using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000BA RID: 186
	[DebuggerDisplay("[VisualAxis] Name={Name}")]
	internal sealed class VisualAxis : IStructuredToString, IEquatable<VisualAxis>
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00007D0F File Offset: 0x00005F0F
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00007D17 File Offset: 0x00005F17
		public string Name { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00007D20 File Offset: 0x00005F20
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00007D28 File Offset: 0x00005F28
		public List<VisualAxisGroup> Groups { get; set; }

		// Token: 0x06000459 RID: 1113 RVA: 0x00007D31 File Offset: 0x00005F31
		public override bool Equals(object obj)
		{
			return this.Equals(obj as VisualAxis);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00007D3F File Offset: 0x00005F3F
		public bool Equals(VisualAxis other)
		{
			return other != null && this.Name == other.Name && this.Groups.SequenceEqualReadOnly(other.Groups);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00007D6A File Offset: 0x00005F6A
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Name, null), Hashing.CombineHashReadonly<VisualAxisGroup>(this.Groups, null));
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00007D89 File Offset: 0x00005F89
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("Axis");
			builder.WriteAttribute<string>("Name", this.Name, false, false);
			builder.WriteProperty<List<VisualAxisGroup>>("Groups", this.Groups, false);
			builder.EndObject();
		}
	}
}
