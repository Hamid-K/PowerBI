using System;
using System.IO;
using System.Xml;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Content;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000288 RID: 648
	internal sealed class XmlModule : Module
	{
		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x06001A89 RID: 6793 RVA: 0x00035736 File Offset: 0x00033936
		public override string Name
		{
			get
			{
				return "Xml";
			}
		}

		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x06001A8A RID: 6794 RVA: 0x0003573D File Offset: 0x0003393D
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Xml.Document";
						}
						if (index != 1)
						{
							throw new InvalidOperationException();
						}
						return "Xml.Tables";
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00035778 File Offset: 0x00033978
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new XmlModule.XmlDocumentFunctionValue();
				}
				if (index != 1)
				{
					throw new InvalidOperationException();
				}
				return new XmlModule.XmlTablesFunctionValue(host);
			});
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000357A9 File Offset: 0x000339A9
		public static ValueException XmlInvalidFormatError(Value detail, Exception exception)
		{
			return ValueException.NewDataFormatError(Strings.XmlError(exception.Message), detail, exception);
		}

		// Token: 0x040007E1 RID: 2017
		private Keys exportKeys;

		// Token: 0x02000289 RID: 649
		private enum Exports
		{
			// Token: 0x040007E3 RID: 2019
			XmlDocument,
			// Token: 0x040007E4 RID: 2020
			XmlTables,
			// Token: 0x040007E5 RID: 2021
			Count
		}

		// Token: 0x0200028A RID: 650
		public sealed class XmlDocumentFunctionValue : NativeFunctionValue2<TableValue, Value, Value>
		{
			// Token: 0x06001A8E RID: 6798 RVA: 0x000357C2 File Offset: 0x000339C2
			public XmlDocumentFunctionValue()
				: base(TypeValue.Table, 1, "contents", TypeValue.Any, "encoding", TextEncoding.Type.Nullable)
			{
			}

			// Token: 0x06001A8F RID: 6799 RVA: 0x000357EC File Offset: 0x000339EC
			public override TableValue TypedInvoke(Value contents, Value encoding)
			{
				ContentHelper.VerifyIsContentType(contents);
				TableValue tableValue;
				try
				{
					tableValue = XmlModule.XmlDocumentFunctionValue.BuildValueFromDocument(XmlModuleHelper.OpenContents(contents, encoding));
				}
				catch (XmlException ex)
				{
					throw XmlModule.XmlInvalidFormatError(contents, ex);
				}
				return tableValue;
			}

			// Token: 0x06001A90 RID: 6800 RVA: 0x00035828 File Offset: 0x00033A28
			private static TableValue BuildValueFromDocument(XmlDocument document)
			{
				return XmlModule.XmlDocumentFunctionValue.BuildValueFromElements(document.ChildNodes);
			}

			// Token: 0x06001A91 RID: 6801 RVA: 0x00035835 File Offset: 0x00033A35
			private static TableValue BuildValueFromElements(XmlNodeList nodes)
			{
				return new XmlElementsListValue(nodes).ToTable(XmlElementsEnumerator.TableType);
			}
		}

		// Token: 0x0200028B RID: 651
		private sealed class XmlTablesFunctionValue : NativeFunctionValue3<TableValue, Value, Value, Value>
		{
			// Token: 0x06001A92 RID: 6802 RVA: 0x00035848 File Offset: 0x00033A48
			public XmlTablesFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "contents", TypeValue.Any, "options", NullableTypeValue.Record, "encoding", TextEncoding.Type.Nullable)
			{
				this.host = host;
			}

			// Token: 0x06001A93 RID: 6803 RVA: 0x0003588B File Offset: 0x00033A8B
			public override TableValue TypedInvoke(Value contents, Value options, Value encoding)
			{
				ContentHelper.VerifyIsContentType(contents);
				return this.NewOrCachedTable(contents, new XmlTableOptions(options), encoding);
			}

			// Token: 0x06001A94 RID: 6804 RVA: 0x000358A4 File Offset: 0x00033AA4
			private TableValue NewOrCachedTable(Value contents, XmlTableOptions options, Value encoding)
			{
				IPersistentCache persistentCache = this.host.GetPersistentCache();
				string text = XmlModuleHelper.XmlDocumentKey(contents, encoding);
				IStorage storage;
				if (!persistentCache.TryGetStorage(text, out storage))
				{
					try
					{
						long num;
						using (XmlReader xmlReader = XmlModuleHelper.OpenContentsForReading(contents, encoding, out num))
						{
							TableValue tableValue = XmlTableValue.FromReader(xmlReader, options);
							IPersistentCache persistentCache2 = this.host.GetPersistentCache();
							if (num <= persistentCache2.MaxEntryLength / 2L)
							{
								storage = persistentCache2.CreateStorage();
								XmlTableValue.Serialize(tableValue, storage, this.host);
								persistentCache.CommitStorage(text, storage);
							}
							return tableValue;
						}
					}
					catch (XmlException ex)
					{
						throw XmlModule.XmlInvalidFormatError(contents, ex);
					}
					catch (IOException ex2)
					{
						throw ValueException.NewDataSourceError(ex2.Message, contents, null);
					}
				}
				return XmlTableValue.Deserialize(storage, this.host);
			}

			// Token: 0x040007E6 RID: 2022
			private IEngineHost host;
		}
	}
}
