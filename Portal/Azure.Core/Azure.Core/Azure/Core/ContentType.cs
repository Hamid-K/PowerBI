using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200003F RID: 63
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ContentType : IEquatable<ContentType>, IEquatable<string>
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00005B4F File Offset: 0x00003D4F
		public static ContentType ApplicationJson { get; } = new ContentType("application/json");

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00005B56 File Offset: 0x00003D56
		public static ContentType ApplicationOctetStream { get; } = new ContentType("application/octet-stream");

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00005B5D File Offset: 0x00003D5D
		public static ContentType TextPlain { get; } = new ContentType("text/plain");

		// Token: 0x060001C7 RID: 455 RVA: 0x00005B64 File Offset: 0x00003D64
		public ContentType(string contentType)
		{
			Argument.AssertNotNull<string>(contentType, "contentType");
			this._contentType = contentType;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005B78 File Offset: 0x00003D78
		public static implicit operator ContentType(string contentType)
		{
			return new ContentType(contentType);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005B80 File Offset: 0x00003D80
		public bool Equals(ContentType other)
		{
			return string.Equals(this._contentType, other._contentType, StringComparison.Ordinal);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005B94 File Offset: 0x00003D94
		[NullableContext(2)]
		public bool Equals(string other)
		{
			return string.Equals(this._contentType, other, StringComparison.Ordinal);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00005BA4 File Offset: 0x00003DA4
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is ContentType)
			{
				ContentType contentType = (ContentType)obj;
				if (this.Equals(contentType))
				{
					return true;
				}
			}
			string text = obj as string;
			return text != null && text.Equals(this._contentType, StringComparison.Ordinal);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005BE4 File Offset: 0x00003DE4
		public override int GetHashCode()
		{
			string contentType = this._contentType;
			if (contentType == null)
			{
				return 0;
			}
			return contentType.GetHashCode();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00005BF7 File Offset: 0x00003DF7
		public static bool operator ==(ContentType left, ContentType right)
		{
			return left.Equals(right);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005C01 File Offset: 0x00003E01
		public static bool operator !=(ContentType left, ContentType right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005C0E File Offset: 0x00003E0E
		public override string ToString()
		{
			return this._contentType ?? "";
		}

		// Token: 0x040000D1 RID: 209
		private readonly string _contentType;
	}
}
