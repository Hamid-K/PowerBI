using System;
using System.Runtime.Serialization;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x0200012E RID: 302
	[Serializable]
	internal class ObjectHandleSerializer : ISerializable
	{
		// Token: 0x06000F22 RID: 3874 RVA: 0x00025C70 File Offset: 0x00023E70
		public ObjectHandleSerializer()
		{
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00025C78 File Offset: 0x00023E78
		public ObjectHandleSerializer(object wrapped)
		{
			this._wrapped = wrapped;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00025C88 File Offset: 0x00023E88
		protected ObjectHandleSerializer(SerializationInfo info, StreamingContext context)
		{
			Type type = null;
			try
			{
				type = (Type)info.GetValue("wrappedtype", typeof(Type));
				this._wrapped = info.GetValue("wrappedvalue", type);
			}
			catch (Exception ex)
			{
				this._wrapped = string.Empty;
				InternalLogger.Info(ex, "ObjectHandleSerializer failed to deserialize object: {0}", new object[] { type });
			}
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00025D00 File Offset: 0x00023F00
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			try
			{
				if (this._wrapped is ISerializable || this._wrapped.GetType().IsSerializable)
				{
					info.AddValue("wrappedtype", this._wrapped.GetType());
					info.AddValue("wrappedvalue", this._wrapped);
				}
				else
				{
					info.AddValue("wrappedtype", typeof(string));
					string text = string.Empty;
					try
					{
						object wrapped = this._wrapped;
						text = ((wrapped != null) ? wrapped.ToString() : null);
					}
					finally
					{
						info.AddValue("wrappedvalue", text ?? string.Empty);
					}
				}
			}
			catch (Exception ex)
			{
				string text2 = "ObjectHandleSerializer failed to serialize object: {0}";
				object[] array = new object[1];
				int num = 0;
				object wrapped2 = this._wrapped;
				array[num] = ((wrapped2 != null) ? wrapped2.GetType() : null);
				InternalLogger.Info(ex, text2, array);
			}
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00025DE0 File Offset: 0x00023FE0
		public object Unwrap()
		{
			return this._wrapped ?? string.Empty;
		}

		// Token: 0x04000408 RID: 1032
		[NonSerialized]
		private readonly object _wrapped;
	}
}
