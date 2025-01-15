using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Serialization;
using Microsoft.Mashup.Evaluator;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001A7E RID: 6782
	internal class RemoteEngineFactory : IRemoteServiceFactory
	{
		// Token: 0x0600AB0E RID: 43790 RVA: 0x002346D4 File Offset: 0x002328D4
		public IRemoteServiceStub CreateStub(IEngineHost engineHost, IMessenger messenger, BinaryWriter proxyInitArgs)
		{
			IEngine engine = engineHost.QueryService<IEngine>();
			proxyInitArgs.WriteArray((from pair in engine.GetDllExtensions()
				select new RemoteEngineFactory.BinaryModule(pair.Key, pair.Value)).ToArray<RemoteEngineFactory.BinaryModule>(), delegate(BinaryWriter writer, RemoteEngineFactory.BinaryModule item)
			{
				item.Serialize(writer);
			});
			proxyInitArgs.WriteList(engine.DisabledModules.ToList<string>(), new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteString));
			proxyInitArgs.WriteList(engine.RemovedModules.ToList<string>(), new Action<BinaryWriter, string>(BinaryReaderWriterExtensions.WriteString));
			proxyInitArgs.WriteInt32(engine.Features.Count);
			foreach (KeyValuePair<string, object> keyValuePair in engine.Features)
			{
				proxyInitArgs.WriteString(keyValuePair.Key);
				new ObjectWriter(proxyInitArgs).WriteObject(keyValuePair.Value);
			}
			return EmptyStub.Instance;
		}

		// Token: 0x0600AB0F RID: 43791 RVA: 0x002347E8 File Offset: 0x002329E8
		public IRemoteServiceProxy CreateProxy(IEngineHost engineHost, IMessenger messenger, BinaryReader proxyInitArgs)
		{
			IEngine version = MashupEngines.Version1;
			List<RemoteEngineFactory.BinaryModule> list = proxyInitArgs.ReadList((BinaryReader reader) => RemoteEngineFactory.BinaryModule.FromReader(reader));
			List<string> list2 = proxyInitArgs.ReadList(new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadString));
			List<string> list3 = proxyInitArgs.ReadList(new Func<BinaryReader, string>(BinaryReaderWriterExtensions.ReadString));
			int num = proxyInitArgs.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string text = proxyInitArgs.ReadString();
				object obj = new ObjectReader(proxyInitArgs).ReadObject();
				version.Features[text] = obj;
			}
			RemoteEngineFactory.UpdateExtensions(version, list);
			RemoteEngineFactory.UpdateModuleList(list2, version.DisabledModules);
			RemoteEngineFactory.UpdateModuleList(list3, version.RemovedModules);
			return new EngineHostServiceProxy(new SimpleEngineHost<IEngine>(version));
		}

		// Token: 0x0600AB10 RID: 43792 RVA: 0x002348B4 File Offset: 0x00232AB4
		private static void UpdateExtensions(IEngine engine, List<RemoteEngineFactory.BinaryModule> extensions)
		{
			foreach (RemoteEngineFactory.BinaryModule binaryModule in extensions)
			{
				binaryModule.EnsureLoaded(engine);
			}
		}

		// Token: 0x0600AB11 RID: 43793 RVA: 0x00234900 File Offset: 0x00232B00
		private static void UpdateModuleList(List<string> sourceList, ICollection<string> targetList)
		{
			HashSet<string> hashSet = new HashSet<string>(targetList);
			bool flag = hashSet.Count == sourceList.Count;
			int num = 0;
			while (num < sourceList.Count && flag)
			{
				flag = hashSet.Contains(sourceList[num]);
				num++;
			}
			if (!flag)
			{
				targetList.Clear();
				foreach (string text in sourceList)
				{
					targetList.Add(text);
				}
				DependencyCompiler.ClearCaches();
			}
		}

		// Token: 0x02001A7F RID: 6783
		private class BinaryModule
		{
			// Token: 0x0600AB13 RID: 43795 RVA: 0x00234998 File Offset: 0x00232B98
			public BinaryModule(string moduleName, string fileName)
			{
				this.moduleName = moduleName;
				this.fileName = fileName;
			}

			// Token: 0x0600AB14 RID: 43796 RVA: 0x002349AE File Offset: 0x00232BAE
			public static RemoteEngineFactory.BinaryModule FromReader(BinaryReader reader)
			{
				return new RemoteEngineFactory.BinaryModule(reader.ReadString(), reader.ReadString());
			}

			// Token: 0x0600AB15 RID: 43797 RVA: 0x002349C1 File Offset: 0x00232BC1
			public void Serialize(BinaryWriter writer)
			{
				writer.WriteString(this.moduleName);
				writer.WriteString(this.fileName);
			}

			// Token: 0x0600AB16 RID: 43798 RVA: 0x002349DC File Offset: 0x00232BDC
			public void EnsureLoaded(IEngine engine)
			{
				object obj = RemoteEngineFactory.BinaryModule.lockObject;
				lock (obj)
				{
					IModule module;
					engine.TryGetModule(this.moduleName, out module);
					if (module == null)
					{
						string text;
						Exception ex;
						if (!engine.TryLoadDllExtension(this.fileName, out text, out ex))
						{
							throw ex;
						}
					}
				}
			}

			// Token: 0x040058B9 RID: 22713
			private static readonly object lockObject = new object();

			// Token: 0x040058BA RID: 22714
			private readonly string moduleName;

			// Token: 0x040058BB RID: 22715
			private readonly string fileName;
		}
	}
}
