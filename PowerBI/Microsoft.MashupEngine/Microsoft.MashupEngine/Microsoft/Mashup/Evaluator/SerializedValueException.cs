using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;
using Microsoft.Mashup.Evaluator.Services;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001D55 RID: 7509
	[Serializable]
	public class SerializedValueException : HostingException
	{
		// Token: 0x0600BACA RID: 47818 RVA: 0x0025D194 File Offset: 0x0025B394
		public static SerializedValueException New(ValueException2 exception, IEngineHost engineHost)
		{
			IEngine engine = engineHost.QueryService<IEngine>();
			IGetStackFrameExtendedInfo getStackFrameExtendedInfo = engineHost.QueryService<IGetStackFrameExtendedInfo>();
			return new SerializedValueException(ValueSerializer.SerializePreviewException(engine, exception, getStackFrameExtendedInfo)).CopyExceptionDataFrom(exception);
		}

		// Token: 0x0600BACB RID: 47819 RVA: 0x0025D1C0 File Offset: 0x0025B3C0
		public SerializedValueException(string serializedException)
			: base(null, "EvaluationError")
		{
			this.serializedException = serializedException;
		}

		// Token: 0x0600BACC RID: 47820 RVA: 0x0025D1D5 File Offset: 0x0025B3D5
		protected SerializedValueException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.serializedException = (string)info.GetValue("serializedValueException", typeof(string));
		}

		// Token: 0x17002E1B RID: 11803
		// (get) Token: 0x0600BACD RID: 47821 RVA: 0x0025D1FF File Offset: 0x0025B3FF
		public string SerializedException
		{
			get
			{
				return this.serializedException;
			}
		}

		// Token: 0x0600BACE RID: 47822 RVA: 0x0025D208 File Offset: 0x0025B408
		public void Throw(IEngineHost engineHost)
		{
			try
			{
				ValueDeserializer.Deserialize(engineHost.QueryService<IEngine>(), this.serializedException);
			}
			catch (ValueException2 valueException)
			{
				valueException.CopyExceptionDataFrom(this);
				throw;
			}
			throw new InvalidOperationException("Deserialized ValueException was not thrown!");
		}

		// Token: 0x0600BACF RID: 47823 RVA: 0x0025D24C File Offset: 0x0025B44C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("serializedValueException", this.serializedException, typeof(string));
			base.GetObjectData(info, context);
		}

		// Token: 0x04005F11 RID: 24337
		private const string serializedExceptionName = "serializedValueException";

		// Token: 0x04005F12 RID: 24338
		private readonly string serializedException;
	}
}
