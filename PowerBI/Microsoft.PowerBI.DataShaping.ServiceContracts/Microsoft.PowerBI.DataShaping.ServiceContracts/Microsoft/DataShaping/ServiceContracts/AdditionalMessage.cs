using System;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.ServiceContracts
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public sealed class AdditionalMessage : IEquatable<AdditionalMessage>
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		public AdditionalMessage(string code, string severity, string message, string objectType = null, string objectName = null, string propertyName = null, string[] affectedItems = null, int? line = null, int? position = null)
		{
			this.Code = code;
			this.Severity = severity;
			this.Message = message;
			this.ObjectType = objectType;
			this.ObjectName = objectName;
			this.PropertyName = propertyName;
			this.AffectedItems = affectedItems;
			this.Line = line;
			this.Position = position;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C0 File Offset: 0x000002C0
		internal string Code { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020C8 File Offset: 0x000002C8
		internal string Severity { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D0 File Offset: 0x000002D0
		internal string Message { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020D8 File Offset: 0x000002D8
		internal string ObjectType { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020E0 File Offset: 0x000002E0
		internal string ObjectName { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020E8 File Offset: 0x000002E8
		internal string PropertyName { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020F0 File Offset: 0x000002F0
		internal string[] AffectedItems { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020F8 File Offset: 0x000002F8
		public int? Position { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002100 File Offset: 0x00000300
		public int? Line { get; }

		// Token: 0x0600000D RID: 13 RVA: 0x00002108 File Offset: 0x00000308
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AdditionalMessage);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002118 File Offset: 0x00000318
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Code.GetHashCode(), this.Severity.GetHashCode(), this.Message.GetHashCode(), this.ObjectType.GetHashCode(), this.ObjectName.GetHashCode(), this.PropertyName.GetHashCode(), Hashing.CombineHash<string>(this.AffectedItems, null), Hashing.GetHashCode<int?>(this.Position, null), Hashing.GetHashCode<int?>(this.Line, null));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002190 File Offset: 0x00000390
		public bool Equals(AdditionalMessage other)
		{
			bool? flag = CompareUtil.AreEqual<AdditionalMessage, AdditionalMessage>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			if (this.Code == other.Code && this.Severity == other.Severity && this.Message == other.Message && this.ObjectType == other.ObjectType && this.ObjectName == other.ObjectName && this.PropertyName == other.PropertyName && this.AffectedItems.SequenceEqualReadOnly(other.AffectedItems))
			{
				int? num = this.Line;
				int? num2 = other.Line;
				if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
				{
					num2 = this.Position;
					num = other.Position;
					return (num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null));
				}
			}
			return false;
		}
	}
}
