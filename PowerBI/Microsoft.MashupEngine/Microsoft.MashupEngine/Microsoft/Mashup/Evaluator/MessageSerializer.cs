using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CF7 RID: 7415
	internal sealed class MessageSerializer : IMessageSerializer
	{
		// Token: 0x0600B916 RID: 47382 RVA: 0x00258414 File Offset: 0x00256614
		public MessageSerializer()
		{
			this.typeToId.Add(typeof(MessageSerializer.RegisterMessageMessage), -2);
			this.idToType.Add(-2, typeof(MessageSerializer.RegisterMessageMessage));
		}

		// Token: 0x0600B917 RID: 47383 RVA: 0x00258478 File Offset: 0x00256678
		public void Serialize(BinaryWriter writer, Message message)
		{
			int num;
			while (!this.typeToId.TryGetValue(message.GetType(), out num))
			{
				Type type = message.GetType();
				num = this.RegisterMessage(type);
				this.Serialize(writer, new MessageSerializer.RegisterMessageMessage
				{
					TypeName = type.FullName,
					AssemblyName = type.Assembly.FullName,
					MessageId = num
				});
			}
			writer.WriteInt32(num);
			message.Serializer = this;
			message.WriteTo(writer);
		}

		// Token: 0x0600B918 RID: 47384 RVA: 0x002584F0 File Offset: 0x002566F0
		public Message Deserialize(BinaryReader reader)
		{
			Message message;
			for (;;)
			{
				int num = reader.ReadInt32();
				message = (Message)Activator.CreateInstance(this.idToType[num]);
				message.Serializer = this;
				message.Deserialize(reader);
				if (num != -2)
				{
					break;
				}
				this.OnRegisterMessage((MessageSerializer.RegisterMessageMessage)message);
			}
			return message;
		}

		// Token: 0x0600B919 RID: 47385 RVA: 0x00258540 File Offset: 0x00256740
		private void OnRegisterMessage(MessageSerializer.RegisterMessageMessage message)
		{
			Type type = (from a in AppDomain.CurrentDomain.GetAssemblies()
				where a.FullName == message.AssemblyName
				select a).Single<Assembly>().GetType(message.TypeName);
			if (!typeof(Message).IsAssignableFrom(type))
			{
				throw new InvalidOperationException("Type '" + type.FullName + "' must be derived from type Message.");
			}
			this.RegisterForeignMessage(type, message.MessageId);
		}

		// Token: 0x0600B91A RID: 47386 RVA: 0x002585CC File Offset: 0x002567CC
		private int RegisterMessage(Type type)
		{
			object obj = this.syncRoot;
			int num;
			lock (obj)
			{
				num = this.nextMessageId;
				this.nextMessageId = num + 1;
				int num2 = num;
				this.typeToId.Add(type, num2);
				num = num2;
			}
			return num;
		}

		// Token: 0x0600B91B RID: 47387 RVA: 0x00258628 File Offset: 0x00256828
		private void RegisterForeignMessage(Type type, int id)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.idToType.Add(id, type);
			}
		}

		// Token: 0x04005E39 RID: 24121
		private const int registerMessageId = -2;

		// Token: 0x04005E3A RID: 24122
		private readonly object syncRoot = new object();

		// Token: 0x04005E3B RID: 24123
		private readonly Dictionary<Type, int> typeToId = new Dictionary<Type, int>();

		// Token: 0x04005E3C RID: 24124
		private readonly Dictionary<int, Type> idToType = new Dictionary<int, Type>();

		// Token: 0x04005E3D RID: 24125
		private int nextMessageId;

		// Token: 0x02001CF8 RID: 7416
		private sealed class RegisterMessageMessage : UnbufferedMessage
		{
			// Token: 0x17002DC8 RID: 11720
			// (get) Token: 0x0600B91C RID: 47388 RVA: 0x00258670 File Offset: 0x00256870
			// (set) Token: 0x0600B91D RID: 47389 RVA: 0x00258678 File Offset: 0x00256878
			public string TypeName { get; set; }

			// Token: 0x17002DC9 RID: 11721
			// (get) Token: 0x0600B91E RID: 47390 RVA: 0x00258681 File Offset: 0x00256881
			// (set) Token: 0x0600B91F RID: 47391 RVA: 0x00258689 File Offset: 0x00256889
			public string AssemblyName { get; set; }

			// Token: 0x17002DCA RID: 11722
			// (get) Token: 0x0600B920 RID: 47392 RVA: 0x00258692 File Offset: 0x00256892
			// (set) Token: 0x0600B921 RID: 47393 RVA: 0x0025869A File Offset: 0x0025689A
			public int MessageId { get; set; }

			// Token: 0x0600B922 RID: 47394 RVA: 0x002586A3 File Offset: 0x002568A3
			public override void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.TypeName);
				writer.WriteString(this.AssemblyName);
				writer.WriteInt32(this.MessageId);
			}

			// Token: 0x0600B923 RID: 47395 RVA: 0x002586C9 File Offset: 0x002568C9
			public override void Deserialize(BinaryReader reader)
			{
				this.TypeName = reader.ReadString();
				this.AssemblyName = reader.ReadString();
				this.MessageId = reader.ReadInt32();
			}
		}
	}
}
