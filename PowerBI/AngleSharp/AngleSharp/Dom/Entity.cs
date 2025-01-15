using System;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x02000153 RID: 339
	internal sealed class Entity : Node
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x000439A0 File Offset: 0x00041BA0
		internal Entity(Document owner)
			: this(owner, string.Empty)
		{
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000439AE File Offset: 0x00041BAE
		internal Entity(Document owner, string name)
			: base(owner, name, NodeType.Entity, NodeFlags.None)
		{
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x000439BA File Offset: 0x00041BBA
		public string PublicId
		{
			get
			{
				return this._publicId;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x000439C2 File Offset: 0x00041BC2
		public string SystemId
		{
			get
			{
				return this._systemId;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x000439CA File Offset: 0x00041BCA
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x000439D2 File Offset: 0x00041BD2
		public string NotationName
		{
			get
			{
				return this._notationName;
			}
			internal set
			{
				this._notationName = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x000439DB File Offset: 0x00041BDB
		public string InputEncoding
		{
			get
			{
				return this._inputEncoding;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x000439E3 File Offset: 0x00041BE3
		public string XmlEncoding
		{
			get
			{
				return this._xmlEncoding;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x000439EB File Offset: 0x00041BEB
		public string XmlVersion
		{
			get
			{
				return this._xmlVersion;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x000439F3 File Offset: 0x00041BF3
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x000439FB File Offset: 0x00041BFB
		public override string TextContent
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x000439F3 File Offset: 0x00041BF3
		// (set) Token: 0x06000BB0 RID: 2992 RVA: 0x000439FB File Offset: 0x00041BFB
		public override string NodeValue
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x00043A04 File Offset: 0x00041C04
		public override INode Clone(bool deep = true)
		{
			Entity entity = new Entity(base.Owner, base.NodeName)
			{
				_xmlEncoding = this._xmlEncoding,
				_xmlVersion = this._xmlVersion,
				_systemId = this._systemId,
				_publicId = this._publicId,
				_inputEncoding = this._inputEncoding,
				_notationName = this._notationName
			};
			base.CloneNode(entity, deep);
			return entity;
		}

		// Token: 0x04000941 RID: 2369
		private string _publicId;

		// Token: 0x04000942 RID: 2370
		private string _systemId;

		// Token: 0x04000943 RID: 2371
		private string _notationName;

		// Token: 0x04000944 RID: 2372
		private string _inputEncoding;

		// Token: 0x04000945 RID: 2373
		private string _xmlVersion;

		// Token: 0x04000946 RID: 2374
		private string _xmlEncoding;

		// Token: 0x04000947 RID: 2375
		private string _value;
	}
}
