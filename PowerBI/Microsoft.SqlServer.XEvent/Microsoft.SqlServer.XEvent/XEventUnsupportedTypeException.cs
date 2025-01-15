using System;
using System.Reflection;
using System.Runtime.Serialization;
using Microsoft.SqlServer.XEvent.Internal;

namespace Microsoft.SqlServer.XEvent
{
	// Token: 0x0200004D RID: 77
	[Serializable]
	public class XEventUnsupportedTypeException : Exception
	{
		// Token: 0x06000184 RID: 388 RVA: 0x00003C74 File Offset: 0x00003C74
		protected XEventUnsupportedTypeException(SerializationInfo info, StreamingContext context)
		{
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00004E44 File Offset: 0x00004E44
		public XEventUnsupportedTypeException(Type containingType, FieldInfo field)
			: base(XEventUnsupportedTypeException.MakeMessage(containingType, field))
		{
			this.m_class = containingType;
			this.m_field = field;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003C20 File Offset: 0x00003C20
		public XEventUnsupportedTypeException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00003C04 File Offset: 0x00003C04
		public XEventUnsupportedTypeException(string message)
			: base(message)
		{
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00003BE8 File Offset: 0x00003BE8
		public XEventUnsupportedTypeException()
		{
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00004E74 File Offset: 0x00004E74
		public override string ToString()
		{
			if (this.m_class != null)
			{
				return XEventUnsupportedTypeException.MakeMessage(this.m_class, this.m_field);
			}
			return this.Message;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00003C3C File Offset: 0x00003C3C
		public virtual Type ContainingType
		{
			get
			{
				return this.m_class;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00003C58 File Offset: 0x00003C58
		public virtual FieldInfo UnsupportedFieldInfo
		{
			get
			{
				return this.m_field;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00003C90 File Offset: 0x00003C90
		protected static string MakeMessage(Type containingType, FieldInfo field)
		{
			return string.Format(ResourcesMgr.GetString("UnsupportedTypeException"), containingType.Name, field.Name, field.FieldType.ToString());
		}

		// Token: 0x04000143 RID: 323
		protected Type m_class;

		// Token: 0x04000144 RID: 324
		protected FieldInfo m_field;
	}
}
