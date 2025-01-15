using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Migrations.Design
{
	// Token: 0x020000E6 RID: 230
	[Obsolete("Use System.Data.Entity.Infrastructure.Design.IErrorHandler instead.")]
	[Serializable]
	public class ToolingException : Exception
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x0002AB3D File Offset: 0x00028D3D
		public ToolingException()
		{
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0002AB4B File Offset: 0x00028D4B
		public ToolingException(string message)
			: base(message)
		{
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0002AB5A File Offset: 0x00028D5A
		public ToolingException(string message, string innerType, string innerStackTrace)
			: base(message)
		{
			this._state.InnerType = innerType;
			this._state.InnerStackTrace = innerStackTrace;
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0002AB81 File Offset: 0x00028D81
		public ToolingException(string message, Exception innerException)
			: base(message, innerException)
		{
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001160 RID: 4448 RVA: 0x0002AB91 File Offset: 0x00028D91
		public string InnerType
		{
			get
			{
				return this._state.InnerType;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0002AB9E File Offset: 0x00028D9E
		public string InnerStackTrace
		{
			get
			{
				return this._state.InnerStackTrace;
			}
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0002ABAB File Offset: 0x00028DAB
		private void SubscribeToSerializeObjectState()
		{
			base.SerializeObjectState += delegate(object _, SafeSerializationEventArgs a)
			{
				a.AddSerializedState(this._state);
			};
		}

		// Token: 0x040008E0 RID: 2272
		[NonSerialized]
		private ToolingException.ToolingExceptionState _state;

		// Token: 0x020007CB RID: 1995
		[Serializable]
		private struct ToolingExceptionState : ISafeSerializationData
		{
			// Token: 0x17001041 RID: 4161
			// (get) Token: 0x06005857 RID: 22615 RVA: 0x00138CA5 File Offset: 0x00136EA5
			// (set) Token: 0x06005858 RID: 22616 RVA: 0x00138CAD File Offset: 0x00136EAD
			public string InnerType { get; set; }

			// Token: 0x17001042 RID: 4162
			// (get) Token: 0x06005859 RID: 22617 RVA: 0x00138CB6 File Offset: 0x00136EB6
			// (set) Token: 0x0600585A RID: 22618 RVA: 0x00138CBE File Offset: 0x00136EBE
			public string InnerStackTrace { get; set; }

			// Token: 0x0600585B RID: 22619 RVA: 0x00138CC7 File Offset: 0x00136EC7
			public void CompleteDeserialization(object deserialized)
			{
				((ToolingException)deserialized)._state = this;
			}
		}
	}
}
