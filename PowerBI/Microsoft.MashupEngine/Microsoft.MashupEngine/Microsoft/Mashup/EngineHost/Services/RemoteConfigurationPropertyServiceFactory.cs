using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A5C RID: 6748
	internal class RemoteConfigurationPropertyServiceFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AA69 RID: 43625 RVA: 0x00232C54 File Offset: 0x00230E54
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IConfigurationPropertyService configurationPropertyService = engineHost.QueryService<IConfigurationPropertyService>();
			if (configurationPropertyService == null)
			{
				proxyInitArgs.Write(0);
			}
			else
			{
				proxyInitArgs.Write(configurationPropertyService.Values.Count);
				foreach (KeyValuePair<string, object> keyValuePair in configurationPropertyService.Values)
				{
					proxyInitArgs.Write(keyValuePair.Key);
					RemoteConfigurationPropertyServiceFactory.WriteObject(proxyInitArgs, keyValuePair.Value);
				}
			}
			return EmptyStub.Instance;
		}

		// Token: 0x0600AA6A RID: 43626 RVA: 0x00232CE0 File Offset: 0x00230EE0
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			int num = proxyInitArgs.ReadInt32();
			Dictionary<string, object> dictionary = new Dictionary<string, object>(num);
			for (int i = 0; i < num; i++)
			{
				string text = proxyInitArgs.ReadString();
				object obj = RemoteConfigurationPropertyServiceFactory.ReadObject(proxyInitArgs);
				dictionary[text] = obj;
			}
			return new EngineHostServiceProxy(new SimpleEngineHost<IConfigurationPropertyService>(new ConfigurationPropertyService(dictionary)));
		}

		// Token: 0x0600AA6B RID: 43627 RVA: 0x00232D30 File Offset: 0x00230F30
		private static void WriteObject(BinaryWriter writer, object obj)
		{
			TypeCode typeCode = ((obj == null) ? TypeCode.Empty : Type.GetTypeCode(obj.GetType()));
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
				writer.Write(0);
				return;
			case TypeCode.Object:
				break;
			case TypeCode.Boolean:
				writer.Write(3);
				writer.Write((bool)obj);
				return;
			default:
				switch (typeCode)
				{
				case TypeCode.Int32:
					writer.Write(9);
					writer.Write((int)obj);
					return;
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Single:
					break;
				case TypeCode.Int64:
					writer.Write(11);
					writer.Write((long)obj);
					return;
				case TypeCode.Double:
					writer.Write(14);
					writer.Write((double)obj);
					return;
				case TypeCode.Decimal:
					writer.Write(15);
					writer.Write((decimal)obj);
					return;
				default:
					if (typeCode == TypeCode.String)
					{
						writer.Write(18);
						writer.Write((string)obj);
						return;
					}
					break;
				}
				break;
			}
			writer.Write(18);
			writer.Write(obj.ToString());
		}

		// Token: 0x0600AA6C RID: 43628 RVA: 0x00232E30 File Offset: 0x00231030
		private static object ReadObject(BinaryReader reader)
		{
			TypeCode typeCode = (TypeCode)reader.ReadInt16();
			switch (typeCode)
			{
			case TypeCode.Empty:
			case TypeCode.DBNull:
				return null;
			case TypeCode.Object:
				break;
			case TypeCode.Boolean:
				return reader.ReadBoolean();
			default:
				switch (typeCode)
				{
				case TypeCode.Int32:
					return reader.ReadInt32();
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Single:
					break;
				case TypeCode.Int64:
					return reader.ReadInt64();
				case TypeCode.Double:
					return reader.ReadDouble();
				case TypeCode.Decimal:
					return reader.ReadDecimal();
				default:
					if (typeCode == TypeCode.String)
					{
						return reader.ReadString();
					}
					break;
				}
				break;
			}
			throw new InvalidOperationException();
		}
	}
}
