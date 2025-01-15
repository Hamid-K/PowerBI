using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Xml;

namespace Microsoft.AnalysisServices.Core
{
	// Token: 0x020000DD RID: 221
	internal class AnalysisServicesClient : XmlaClient
	{
		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002E358 File Offset: 0x0002C558
		public static void WriteCreate(XmlWriter output, IMajorObject parent, IMajorObject obj, ObjectExpansion expansion, bool allowOverwrite, JaXmlSerializer serializer)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			output.WriteStartElement("Create", "http://schemas.microsoft.com/analysisservices/2003/engine");
			if (allowOverwrite)
			{
				output.WriteAttributeString("AllowOverwrite", XmlConvert.ToString(allowOverwrite));
			}
			if (parent != null && !(parent is Server))
			{
				output.WriteStartElement("ParentObject");
				parent.WriteRef(output);
				output.WriteEndElement();
			}
			output.WriteStartElement("ObjectDefinition");
			try
			{
				((MajorObject)obj).internalState = MajorObjectState.Saving;
				serializer.SerializeComponent(output, obj.BaseType, obj);
			}
			finally
			{
				((MajorObject)obj).internalState = MajorObjectState.Ready;
			}
			output.WriteEndElement();
			output.WriteEndElement();
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0002E41C File Offset: 0x0002C61C
		public static void WriteAlter(XmlWriter output, IMajorObject obj, ObjectExpansion expansion, bool allowCreate, JaXmlSerializer serializer)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			output.WriteStartElement("Alter", "http://schemas.microsoft.com/analysisservices/2003/engine");
			if (allowCreate)
			{
				output.WriteAttributeString("AllowCreate", "true");
			}
			output.WriteAttributeString("ObjectExpansion", (expansion == ObjectExpansion.Full) ? "ExpandFull" : "ObjectProperties");
			output.WriteStartElement("Object");
			obj.WriteRef(output);
			output.WriteEndElement();
			output.WriteStartElement("ObjectDefinition");
			try
			{
				((MajorObject)obj).internalState = MajorObjectState.Saving;
				serializer.SerializeComponent(output, obj.BaseType, obj);
			}
			finally
			{
				((MajorObject)obj).internalState = MajorObjectState.Ready;
			}
			output.WriteEndElement();
			output.WriteEndElement();
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0002E4EC File Offset: 0x0002C6EC
		public static void WriteDelete(XmlWriter output, IMajorObject obj, bool ignoreFailures)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			output.WriteStartElement("Delete", "http://schemas.microsoft.com/analysisservices/2003/engine");
			if (ignoreFailures)
			{
				output.WriteAttributeString("IgnoreFailures", XmlConvert.ToString(ignoreFailures));
			}
			output.WriteStartElement("Object");
			obj.WriteRef(output);
			output.WriteEndElement();
			output.WriteEndElement();
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0002E558 File Offset: 0x0002C758
		public static void WriteProcess(XmlWriter output, IMajorObject obj, ProcessType type, IBinding source, ErrorConfiguration errorConfig, WriteBackTableCreation writebackOption, JaXmlSerializer serializer)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			output.WriteStartElement("Process", "http://schemas.microsoft.com/analysisservices/2003/engine");
			output.WriteElementString("Type", type.ToString());
			output.WriteStartElement("Object");
			obj.WriteRef(output);
			output.WriteEndElement();
			if (source != null)
			{
				output.WriteStartElement("Bindings");
				output.WriteStartElement("Binding");
				obj.WriteRef(output);
				output.WriteStartElement("Source");
				output.WriteAttributeString("type", "http://www.w3.org/2001/XMLSchema-instance", source.GetType().Name);
				if (!(source is IQueryBinding))
				{
					throw new NotImplementedException();
				}
				output.WriteElementString("DataSourceID", ((IQueryBinding)source).DataSourceID);
				output.WriteElementString("QueryDefinition", ((IQueryBinding)source).QueryDefinition);
				output.WriteEndElement();
				output.WriteEndElement();
				output.WriteEndElement();
			}
			if (errorConfig != null)
			{
				serializer.Serialize(output, errorConfig);
			}
			if (writebackOption != WriteBackTableCreation.UseExisting)
			{
				output.WriteElementString("WriteBackTableCreation", writebackOption.ToString());
			}
			output.WriteEndElement();
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0002E689 File Offset: 0x0002C889
		public AnalysisServicesClient(Server owner)
			: base(owner)
		{
			this.owner = owner;
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0002E699 File Offset: 0x0002C899
		public AnalysisServicesClient(Server owner, StringCollection log)
			: base(owner, log)
		{
			this.owner = owner;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0002E6AC File Offset: 0x0002C8AC
		public void Create(IMajorObject parent, IMajorObject obj, ObjectExpansion expansion, ImpactDetailCollection impact, bool allowOverwrite, XmlaWarningCollection warnings, JaXmlSerializer serializer)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				AnalysisServicesClient.WriteCreate(this.writer, parent, obj, expansion, allowOverwrite, serializer);
				this.WriteEndCommand(impact != null, dictionary);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.CopyXmlaWarnings(this.SendExecuteAndReadResponse(impact, true, true), warnings);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0002E750 File Offset: 0x0002C950
		internal void WriteCreate(IMajorObject obj, ObjectExpansion expansion, bool allowOverwrite, JaXmlSerializer serializer)
		{
			AnalysisServicesClient.WriteCreate(this.writer, (IMajorObject)obj.Parent, obj, expansion, allowOverwrite, serializer);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0002E76D File Offset: 0x0002C96D
		internal void WriteAlter(IMajorObject obj, ObjectExpansion expansion, bool allowCreate, JaXmlSerializer serializer)
		{
			AnalysisServicesClient.WriteAlter(this.writer, obj, expansion, allowCreate, serializer);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0002E780 File Offset: 0x0002C980
		public void Alter(IMajorObject obj, ObjectExpansion expansion, ImpactDetailCollection impact, bool allowCreate, XmlaWarningCollection warnings, JaXmlSerializer serializer)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				AnalysisServicesClient.WriteAlter(this.writer, obj, expansion, allowCreate, serializer);
				this.WriteEndCommand(impact != null, dictionary);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.CopyXmlaWarnings(this.SendExecuteAndReadResponse(impact, true, true), warnings);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0002E820 File Offset: 0x0002CA20
		public void Alter(IMajorObject[] objects, ImpactDetailCollection impact, bool transactional, JaXmlSerializer serializer)
		{
			base.CheckConnection();
			if (objects == null)
			{
				throw new ArgumentNullException("objects");
			}
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				base.WriteStartBatch(transactional, false, false);
				foreach (IMajorObject majorObject in objects)
				{
					AnalysisServicesClient.WriteAlter(this.writer, majorObject, ObjectExpansion.Full, true, serializer);
				}
				base.WriteEndBatch();
				this.WriteEndCommand(impact != null, dictionary);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(impact, true, true);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0002E8E4 File Offset: 0x0002CAE4
		public void Delete(IMajorObject obj, ImpactDetailCollection impact, bool ignoreFailures)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				AnalysisServicesClient.WriteDelete(this.writer, obj, ignoreFailures);
				this.WriteEndCommand(impact != null, dictionary);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(impact, true, true);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0002E978 File Offset: 0x0002CB78
		public void Process(IMajorObject obj, ProcessType type, IBinding source, ErrorConfiguration errorConfig, WriteBackTableCreation writebackOption, ImpactDetailCollection impact, XmlaWarningCollection warnings, JaXmlSerializer serializer)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				AnalysisServicesClient.WriteProcess(this.writer, obj, type, source, errorConfig, writebackOption, serializer);
				this.WriteEndCommand(impact != null, dictionary);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.CopyXmlaWarnings(this.SendExecuteAndReadResponse(impact, true, true), warnings);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0002EA1C File Offset: 0x0002CC1C
		public XmlReader Discover(string requestType, IDictionary restrictions)
		{
			return base.Discover(requestType, null, base.ConnectionInfo.ExtendedProperties, restrictions, false, null);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0002EA34 File Offset: 0x0002CC34
		public MajorObject Discover(IMajorObject obj, ObjectExpansion expansion, JaXmlSerializer serializer)
		{
			base.CheckConnection();
			bool captureXml = base.CaptureXml;
			base.CaptureXml = false;
			MajorObject majorObject;
			try
			{
				try
				{
					string xmlaFromObjectExpansion = AnalysisServicesClient.GetXmlaFromObjectExpansion(expansion);
					base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Discover\"");
					base.WriteStartDiscover("DISCOVER_XML_METADATA", null);
					this.writer.WriteStartElement("Restrictions");
					this.writer.WriteStartElement("RestrictionList");
					if (!(obj is Server))
					{
						obj.WriteRef(this.writer);
					}
					this.writer.WriteElementString("ObjectExpansion", xmlaFromObjectExpansion);
					this.writer.WriteEndElement();
					this.writer.WriteEndElement();
					base.WriteEndDiscover(base.ConnectionInfo.ExtendedProperties);
					base.EndMessage();
				}
				catch (IOException ex)
				{
					base.CloseAll();
					throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
				}
				catch
				{
					base.HandleMessageCreationException();
					throw;
				}
				base.SendMessage(true, false, false);
				try
				{
					this.reader.ReadStartElement("DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
					this.reader.ReadStartElement("return");
					this.reader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
					if (this.reader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
					{
						this.reader.Skip();
					}
					if (!this.reader.IsStartElement())
					{
						base.EndReceival();
						majorObject = null;
					}
					else
					{
						this.reader.ReadStartElement("row");
						if (this.reader.IsStartElement("METADATA", "urn:schemas-microsoft-com:xml-analysis:rowset") && this.reader.IsEmptyElement)
						{
							base.EndReceival();
							majorObject = null;
						}
						else
						{
							this.reader.ReadStartElement("METADATA", "urn:schemas-microsoft-com:xml-analysis:rowset");
							XmlaClient.CheckForRowsetError(this.reader, new XmlaResult(), true);
							try
							{
								IDesignerSerializationManager designerSerializationManager = null;
								XmlReader reader = this.reader;
								Type baseType = obj.BaseType;
								XmlaReader xmlaReader = this.reader as XmlaReader;
								obj = (IMajorObject)serializer.DeserializeComponent(designerSerializationManager, reader, baseType, xmlaReader != null && xmlaReader.IsBinaryReader);
								bool flag = this.owner != null;
								ConnectionInfo connectionInfo = base.ConnectionInfo;
								bool flag2 = flag && connectionInfo != null;
								bool flag3 = false;
								if (flag2 && !connectionInfo.IsPbiPremiumXmlaEp && !flag3)
								{
									if (obj.BaseType == typeof(Database) || obj.BaseType.IsSubclassOf(typeof(Database)))
									{
										string text;
										if (!Utils.IsValidNameCharsForDatabaseWithoutPbiPublicXmla((obj as Database).Name, out text))
										{
											throw new InvalidOperationException(text);
										}
									}
									else if (obj.BaseType == typeof(Server) || obj.BaseType.IsSubclassOf(typeof(Server)))
									{
										using (IEnumerator<Database> enumerator = (obj as Server).GetDatabases().GetEnumerator())
										{
											while (enumerator.MoveNext())
											{
												string text2;
												if (!Utils.IsValidNameCharsForDatabaseWithoutPbiPublicXmla(enumerator.Current.Name, out text2))
												{
													throw new InvalidOperationException(text2);
												}
											}
										}
									}
								}
							}
							catch (XmlSerializationException)
							{
								XmlaClient.CheckForException(this.reader, new XmlaResult(), true);
								throw;
							}
							XmlaClient.CheckEndElement(this.reader, "METADATA");
							this.reader.ReadEndElement();
							this.reader.ReadEndElement();
							XmlaClient.CheckForException(this.reader, new XmlaResult(), true);
							if (this.reader.IsStartElement("Messages", "urn:schemas-microsoft-com:xml-analysis:exception"))
							{
								XmlaClient.ReadXmlaMessages(this.reader, new XmlaMessageCollection());
							}
							this.reader.ReadEndElement();
							this.reader.ReadEndElement();
							this.reader.ReadEndElement();
							this.reader.ReadEndElement();
							XmlaClient.CheckEndElement(this.reader, "Envelope");
							this.reader.ReadEndElement();
							base.EndReceival();
							majorObject = (MajorObject)obj;
						}
					}
				}
				catch (IOException ex2)
				{
					base.CloseAll();
					throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
				}
				catch (XmlException ex3)
				{
					base.EndReceival();
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex3);
				}
				catch
				{
					base.EndReceival();
					throw;
				}
			}
			finally
			{
				base.CaptureXml = captureXml;
			}
			return majorObject;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
		protected static string GetXmlaFromObjectExpansion(ObjectExpansion expansion)
		{
			switch (expansion)
			{
			case ObjectExpansion.Full:
				return "ExpandFull";
			case ObjectExpansion.Partial:
				return "ExpandObject";
			case ObjectExpansion.ObjectProperties:
				return "ObjectProperties";
			case ObjectExpansion.ReferenceOnly:
				return "ReferenceOnly";
			default:
				return "ExpandObject";
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x0002EF17 File Offset: 0x0002D117
		protected virtual void ReadImpact(XmlReader reader, ImpactDetailCollection impacts)
		{
			reader.Skip();
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x0002EF20 File Offset: 0x0002D120
		public DateTime GetLastSchemaUpdate(IMajorObject obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			bool captureXml = base.CaptureXml;
			base.CaptureXml = false;
			DateTime dateTime;
			try
			{
				try
				{
					base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Discover\"");
					base.WriteStartDiscover("DISCOVER_XML_METADATA", null);
					this.writer.WriteStartElement("Restrictions");
					this.writer.WriteStartElement("RestrictionList");
					if (!(obj is Server))
					{
						obj.WriteRef(this.writer);
					}
					this.writer.WriteElementString("ObjectExpansion", "ReferenceOnly");
					this.writer.WriteEndElement();
					this.writer.WriteEndElement();
					base.WriteEndDiscover(base.ConnectionInfo.ExtendedProperties);
					base.EndMessage();
				}
				catch (IOException ex)
				{
					base.CloseAll();
					throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
				}
				catch
				{
					base.HandleMessageCreationException();
					throw;
				}
				try
				{
					base.SendMessage(true, false, false);
					try
					{
						this.reader.ReadStartElement("DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
						this.reader.ReadStartElement("return");
						this.reader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
						if (this.reader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
						{
							this.reader.Skip();
						}
						this.reader.MoveToContent();
						if (this.reader.NodeType == XmlNodeType.EndElement)
						{
							dateTime = DateTime.MinValue;
						}
						else
						{
							this.reader.ReadStartElement("row");
							if (this.reader.IsStartElement("METADATA", "urn:schemas-microsoft-com:xml-analysis:rowset") && this.reader.IsEmptyElement)
							{
								dateTime = DateTime.MinValue;
							}
							else
							{
								this.reader.ReadStartElement("METADATA", "urn:schemas-microsoft-com:xml-analysis:rowset");
								XmlaClient.CheckForRowsetError(this.reader, new XmlaResult(), true);
								this.reader.ReadStartElement();
								while (this.reader.IsStartElement())
								{
									if (this.reader.IsStartElement("LastSchemaUpdate"))
									{
										return XmlConvert.ToDateTime(this.reader.ReadElementString(), XmlDateTimeSerializationMode.Utc).ToLocalTime();
									}
									this.reader.Skip();
								}
								dateTime = DateTime.MinValue;
							}
						}
					}
					finally
					{
						base.EndReceival();
					}
				}
				catch (XmlException ex2)
				{
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex2);
				}
				catch (IOException ex3)
				{
					base.CloseAll();
					throw new ConnectionException(XmlaSR.ConnectionBroken, ex3);
				}
			}
			finally
			{
				base.CaptureXml = captureXml;
			}
			return dateTime;
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x0002F20C File Offset: 0x0002D40C
		internal void RefreshStateAndLastProcessed(ProcessableMajorObject obj)
		{
			base.CheckConnection();
			if (base.CaptureXml)
			{
				return;
			}
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Discover\"");
				base.WriteStartDiscover("DISCOVER_XML_METADATA", null);
				this.writer.WriteStartElement("Restrictions");
				this.writer.WriteStartElement("RestrictionList");
				((IMajorObject)obj).WriteRef(this.writer);
				this.writer.WriteElementString("ObjectExpansion", "ReferenceOnly");
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				base.WriteEndDiscover(base.ConnectionInfo.ExtendedProperties);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			try
			{
				base.SendMessage(true, false, false);
				try
				{
					this.reader.ReadStartElement("DiscoverResponse", "urn:schemas-microsoft-com:xml-analysis");
					this.reader.ReadStartElement("return");
					this.reader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:rowset");
					if (this.reader.IsStartElement("schema", "http://www.w3.org/2001/XMLSchema"))
					{
						this.reader.Skip();
					}
					this.reader.ReadStartElement("row");
					if (this.reader.IsStartElement("METADATA", "urn:schemas-microsoft-com:xml-analysis:rowset") && this.reader.IsEmptyElement)
					{
						throw new AmoException(SR.ObjectDoesNotExistOrYouDontHaveReadPermissions);
					}
					this.reader.ReadStartElement("METADATA", "urn:schemas-microsoft-com:xml-analysis:rowset");
					XmlaClient.CheckForRowsetError(this.reader, new XmlaResult(), true);
					this.reader.ReadStartElement();
					while (this.reader.IsStartElement())
					{
						if (this.reader.IsStartElement("State"))
						{
							obj.State = (AnalysisState)Enum.Parse(typeof(AnalysisState), this.reader.ReadElementString());
						}
						else if (this.reader.IsStartElement("LastProcessed"))
						{
							obj.LastProcessed = XmlConvert.ToDateTime(this.reader.ReadElementString(), XmlDateTimeSerializationMode.Utc).ToLocalTime();
						}
						else
						{
							this.reader.Skip();
						}
					}
				}
				finally
				{
					base.EndReceival();
				}
			}
			catch (XmlException ex2)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex2);
			}
			catch (IOException ex3)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex3);
			}
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x0002F4DC File Offset: 0x0002D6DC
		internal void Export(Database database, ExportLayout exportLayout, ExportType exportType)
		{
			base.CheckConnection();
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("Export", "http://schemas.microsoft.com/analysisservices/2003/engine");
				this.writer.WriteStartElement("Object");
				((IMajorObject)database).WriteRef(this.writer);
				this.writer.WriteEndElement();
				if (exportLayout != ExportLayout.Delta)
				{
					this.writer.WriteElementString("Layout", exportLayout.ToString());
				}
				if (exportType != ExportType.Full)
				{
					this.writer.WriteElementString("Type", exportType.ToString());
				}
				this.writer.WriteEndElement();
				base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, dictionary, null);
				base.EndMessage();
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(null, true, true);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x0002F5DC File Offset: 0x0002D7DC
		public void Backup(Database database, string file, bool allowOverwrite, bool backupRemotePartitions, ICollection locations, bool applyCompression, string password)
		{
			base.CheckConnection();
			if (database == null)
			{
				throw new ArgumentNullException("database");
			}
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("Backup", "http://schemas.microsoft.com/analysisservices/2003/engine");
				this.writer.WriteStartElement("Object");
				((IMajorObject)database).WriteRef(this.writer);
				this.writer.WriteEndElement();
				this.writer.WriteElementString("File", file);
				if (allowOverwrite)
				{
					this.writer.WriteElementString("AllowOverwrite", XmlConvert.ToString(allowOverwrite));
				}
				if (backupRemotePartitions)
				{
					this.writer.WriteElementString("BackupRemotePartitions", XmlConvert.ToString(backupRemotePartitions));
				}
				if (locations != null && locations.Count > 0)
				{
					this.writer.WriteStartElement("Locations");
					foreach (object obj in locations)
					{
						BackupLocation backupLocation = (BackupLocation)obj;
						this.writer.WriteStartElement("Location");
						if (backupLocation.File != null)
						{
							this.writer.WriteElementString("File", backupLocation.File);
						}
						if (backupLocation.DataSourceID != null)
						{
							this.writer.WriteElementString("DataSourceID", backupLocation.DataSourceID);
						}
						this.writer.WriteEndElement();
					}
					this.writer.WriteEndElement();
				}
				if (!applyCompression)
				{
					this.writer.WriteElementString("ApplyCompression", XmlConvert.ToString(applyCompression));
				}
				if (password != null)
				{
					this.writer.WriteElementString("Password", password);
				}
				this.writer.WriteEndElement();
				base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, dictionary, null);
				base.EndMessage();
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(null, true, true);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0002F7EC File Offset: 0x0002D9EC
		internal void Restore(string file, string databaseName, string databaseID, bool allowOverwrite, ICollection locations, RestoreSecurity security, string password, string dbStorageLocation, ReadWriteMode readWriteMode, bool ignoreIncompatibilities, bool forceRestore)
		{
			base.CheckConnection();
			databaseName = Utils.Trim(databaseName);
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("Restore", "http://schemas.microsoft.com/analysisservices/2003/engine");
				this.writer.WriteElementString("File", file);
				if (!string.IsNullOrEmpty(databaseName))
				{
					this.writer.WriteElementString("DatabaseName", databaseName);
				}
				if (!string.IsNullOrEmpty(databaseID) && !base.ConnectionInfo.IsPbiPremiumXmlaEp)
				{
					this.writer.WriteElementString("DatabaseID", databaseID);
				}
				if (allowOverwrite)
				{
					this.writer.WriteElementString("AllowOverwrite", XmlConvert.ToString(allowOverwrite));
				}
				if (readWriteMode != ReadWriteMode.ReadWrite)
				{
					this.writer.WriteElementString("ReadWriteMode", "http://schemas.microsoft.com/analysisservices/2008/engine/100", readWriteMode.ToString());
				}
				if (locations != null && locations.Count > 0)
				{
					this.writer.WriteStartElement("Locations");
					foreach (object obj in locations)
					{
						RestoreLocation restoreLocation = (RestoreLocation)obj;
						this.writer.WriteStartElement("Location");
						if (restoreLocation.File != null)
						{
							this.writer.WriteElementString("File", restoreLocation.File);
						}
						if (restoreLocation.DataSourceID != null)
						{
							this.writer.WriteElementString("DataSourceID", restoreLocation.DataSourceID);
						}
						if (restoreLocation.DataSourceType != RestoreDataSourceType.Remote)
						{
							this.writer.WriteElementString("DataSourceType", restoreLocation.DataSourceType.ToString());
						}
						if (restoreLocation.ConnectionString != null)
						{
							this.writer.WriteElementString("ConnectionString", restoreLocation.ConnectionString);
						}
						if (restoreLocation.Folders.Count > 0)
						{
							this.writer.WriteStartElement("Folders");
							foreach (object obj2 in ((IEnumerable)restoreLocation.Folders))
							{
								RestoreFolder restoreFolder = (RestoreFolder)obj2;
								this.writer.WriteStartElement("Folder");
								if (restoreFolder.Original != null)
								{
									this.writer.WriteElementString("Original", restoreFolder.Original);
								}
								if (restoreFolder.New != null)
								{
									this.writer.WriteElementString("New", restoreFolder.New);
								}
								this.writer.WriteEndElement();
							}
							this.writer.WriteEndElement();
						}
						this.writer.WriteEndElement();
					}
					this.writer.WriteEndElement();
				}
				if (security != RestoreSecurity.CopyAll)
				{
					this.writer.WriteElementString("Security", security.ToString());
				}
				if (password != null)
				{
					this.writer.WriteElementString("Password", password);
				}
				if (!string.IsNullOrEmpty(dbStorageLocation))
				{
					this.writer.WriteElementString("DbStorageLocation", "http://schemas.microsoft.com/analysisservices/2008/engine/100/100", dbStorageLocation);
				}
				if (ignoreIncompatibilities)
				{
					this.writer.WriteElementString("IgnoreIncompatibilities", "http://schemas.microsoft.com/analysisservices/2020/engine/920", XmlConvert.ToString(ignoreIncompatibilities));
				}
				if (forceRestore)
				{
					this.writer.WriteElementString("ForceRestore", "http://schemas.microsoft.com/analysisservices/2022/engine/922", XmlConvert.ToString(forceRestore));
				}
				this.writer.WriteEndElement();
				base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, dictionary, null);
				base.EndMessage();
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(null, true, true);
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x0002FBB0 File Offset: 0x0002DDB0
		internal void Attach(string folder, ReadWriteMode readWriteMode, string password)
		{
			base.CheckConnection();
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("Attach", "http://schemas.microsoft.com/analysisservices/2003/engine");
				this.writer.WriteElementString("Folder", folder);
				this.writer.WriteElementString("ReadWriteMode", "http://schemas.microsoft.com/analysisservices/2008/engine/100", readWriteMode.ToString());
				if (!string.IsNullOrEmpty(password))
				{
					this.writer.WriteElementString("Password", password);
				}
				this.writer.WriteEndElement();
				base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, dictionary, null);
				base.EndMessage();
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(null, true, true);
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x0002FC84 File Offset: 0x0002DE84
		internal void Detach(Database db, string password)
		{
			base.CheckConnection();
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("Detach", "http://schemas.microsoft.com/analysisservices/2003/engine");
				this.writer.WriteStartElement("Object");
				((IMajorObject)db).WriteRef(this.writer);
				this.writer.WriteEndElement();
				if (!string.IsNullOrEmpty(password))
				{
					this.writer.WriteElementString("Password", password);
				}
				this.writer.WriteEndElement();
				base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, dictionary, null);
				base.EndMessage();
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(null, true, true);
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0002FD4C File Offset: 0x0002DF4C
		internal void BeginTransaction()
		{
			base.CheckConnection();
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				this.writer.WriteElementString("BeginTransaction", "http://schemas.microsoft.com/analysisservices/2003/engine", string.Empty);
				base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, dictionary, null);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			base.SendExecuteAndReadResponse(false, true);
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0002FDEC File Offset: 0x0002DFEC
		internal void RollbackTransaction()
		{
			base.CheckConnection();
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				this.writer.WriteElementString("RollbackTransaction", "http://schemas.microsoft.com/analysisservices/2003/engine", string.Empty);
				base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, dictionary, null);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			base.SendExecuteAndReadResponse(false, true);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0002FE8C File Offset: 0x0002E08C
		internal void CommitTransaction()
		{
			base.CheckConnection();
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				this.writer.WriteElementString("CommitTransaction", "http://schemas.microsoft.com/analysisservices/2003/engine", string.Empty);
				base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, dictionary, null);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			base.SendExecuteAndReadResponse(false, true);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0002FF2C File Offset: 0x0002E12C
		public XmlReader Subscribe(string traceId, string subscriptionId = null)
		{
			base.CheckConnection();
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("Subscribe", "http://schemas.microsoft.com/analysisservices/2003/engine");
				if (!string.IsNullOrEmpty(traceId))
				{
					this.writer.WriteStartElement("Object");
					this.writer.WriteElementString("TraceID", traceId);
					this.writer.WriteEndElement();
				}
				if (!string.IsNullOrEmpty(subscriptionId))
				{
					this.writer.WriteElementString("SubscriptionId", "http://schemas.microsoft.com/analysisservices/2018/engine/800", subscriptionId);
				}
				this.writer.WriteEndElement();
				base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, dictionary, null);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			XmlReader xmlReader;
			try
			{
				if (!this.captureXml)
				{
					xmlReader = base.SendMessage(true, false, false);
				}
				else
				{
					xmlReader = null;
				}
			}
			catch (XmlException ex2)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex2);
			}
			catch (IOException ex3)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex3);
			}
			return xmlReader;
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00030070 File Offset: 0x0002E270
		public void Unsubscribe(string subscriptionId)
		{
			if (this.writer != null || this.reader != null || base.IsReaderDetached)
			{
				XmlaClient xmlaClient = new XmlaClient(this.owner);
				try
				{
					if (string.IsNullOrEmpty(base.SessionID))
					{
						xmlaClient.Connect(new ConnectionInfo(base.ConnectionInfo), false);
					}
					else
					{
						xmlaClient.Connect(new ConnectionInfo(base.ConnectionInfo), base.SessionID);
					}
					AnalysisServicesClient.UnsubscribeImpl(xmlaClient, subscriptionId);
					return;
				}
				finally
				{
					xmlaClient.CloseAll();
				}
			}
			AnalysisServicesClient.UnsubscribeImpl(this, subscriptionId);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x00030100 File Offset: 0x0002E300
		private static void UnsubscribeImpl(XmlaClient client, string subscriptionId)
		{
			client.CheckConnection();
			try
			{
				XmlWriter xmlWriter = client.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				client.WriteStartCommand(ref dictionary);
				xmlWriter.WriteStartElement("Unsubscribe", "http://schemas.microsoft.com/analysisservices/2003/engine");
				xmlWriter.WriteElementString("SubscriptionId", "http://schemas.microsoft.com/analysisservices/2018/engine/800", subscriptionId);
				xmlWriter.WriteEndElement();
				client.WriteEndCommand(client.ConnectionInfo.ExtendedProperties, dictionary, null);
				client.EndMessage();
			}
			catch (IOException ex)
			{
				client.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				client.HandleMessageCreationException();
				throw;
			}
			try
			{
				client.SendExecuteAndReadResponse(true, true, false);
			}
			catch (XmlException ex2)
			{
				throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex2);
			}
			catch (IOException ex3)
			{
				client.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex3);
			}
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x000301E8 File Offset: 0x0002E3E8
		internal void RenameTable(string databaseId, string dimensionId, string name, FixUpExpressions fixupExpressions)
		{
			ListDictionary listDictionary = AnalysisServicesClient.CreateTableObjectReference(databaseId, dimensionId, name);
			this.Rename(listDictionary, name, fixupExpressions);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00030208 File Offset: 0x0002E408
		internal void RenameTableColumn(string databaseId, string dimensionId, string dimensionAttributeId, string name, FixUpExpressions fixupExpressions)
		{
			ListDictionary listDictionary = AnalysisServicesClient.CreateColumnObjectReference(databaseId, dimensionId, dimensionAttributeId, name);
			this.Rename(listDictionary, name, fixupExpressions);
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0003022C File Offset: 0x0002E42C
		internal void RenameScriptMeasure(string databaseId, string dimensionId, string scriptMeasureName, string name, FixUpExpressions fixupExpressions)
		{
			ListDictionary listDictionary = AnalysisServicesClient.CreateScriptMeasureObjectReference(databaseId, dimensionId, scriptMeasureName, name);
			this.Rename(listDictionary, name, fixupExpressions);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00030250 File Offset: 0x0002E450
		internal static void WriteRenameTable(XmlWriter output, string databaseId, string dimensionId, string name, FixUpExpressions fixupExpressions)
		{
			ListDictionary listDictionary = AnalysisServicesClient.CreateTableObjectReference(databaseId, dimensionId, name);
			AnalysisServicesClient.WriteRename(output, listDictionary, name, fixupExpressions);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00030270 File Offset: 0x0002E470
		internal static void WriteRenameTableColumn(XmlWriter output, string databaseId, string dimensionId, string dimensionAttributeId, string name, FixUpExpressions fixupExpressions)
		{
			ListDictionary listDictionary = AnalysisServicesClient.CreateColumnObjectReference(databaseId, dimensionId, dimensionAttributeId, name);
			AnalysisServicesClient.WriteRename(output, listDictionary, name, fixupExpressions);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00030294 File Offset: 0x0002E494
		internal static void WriteRenameScriptMeasure(XmlWriter output, string databaseId, string dimensionId, string measureName, string name, FixUpExpressions fixupExpressions)
		{
			ListDictionary listDictionary = AnalysisServicesClient.CreateScriptMeasureObjectReference(databaseId, dimensionId, measureName, name);
			AnalysisServicesClient.WriteRename(output, listDictionary, name, fixupExpressions);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x000302B8 File Offset: 0x0002E4B8
		private static ListDictionary CreateTableObjectReference(string databaseId, string dimensionId, string name)
		{
			if (string.IsNullOrEmpty(databaseId))
			{
				throw new ArgumentException("databaseId");
			}
			if (string.IsNullOrEmpty(dimensionId))
			{
				throw new ArgumentException("dimensionId");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name");
			}
			return new ListDictionary
			{
				{ "DatabaseID", databaseId },
				{ "TableID", dimensionId }
			};
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003031C File Offset: 0x0002E51C
		private static ListDictionary CreateColumnObjectReference(string databaseId, string dimensionId, string dimensionAttributeId, string name)
		{
			if (string.IsNullOrEmpty(databaseId))
			{
				throw new ArgumentException("databaseId");
			}
			if (string.IsNullOrEmpty(dimensionId))
			{
				throw new ArgumentException("dimensionId");
			}
			if (string.IsNullOrEmpty(dimensionAttributeId))
			{
				throw new ArgumentException("dimensionAttributeId");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name");
			}
			return new ListDictionary
			{
				{ "DatabaseID", databaseId },
				{ "TableID", dimensionId },
				{ "ColumnID", dimensionAttributeId }
			};
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x000303A0 File Offset: 0x0002E5A0
		private static ListDictionary CreateScriptMeasureObjectReference(string databaseId, string dimensionId, string scriptMeasureName, string name)
		{
			if (string.IsNullOrEmpty(databaseId))
			{
				throw new ArgumentException("databaseId");
			}
			if (string.IsNullOrEmpty(dimensionId))
			{
				throw new ArgumentException("dimensionId");
			}
			if (string.IsNullOrEmpty(scriptMeasureName))
			{
				throw new ArgumentException("scriptMeasureName");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name");
			}
			return new ListDictionary
			{
				{ "DatabaseID", databaseId },
				{ "TableID", dimensionId },
				{ "ScriptMeasureName", scriptMeasureName }
			};
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00030424 File Offset: 0x0002E624
		private void Rename(IDictionary objectReference, string newName, FixUpExpressions fixupExpressions)
		{
			base.CheckConnection();
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				AnalysisServicesClient.WriteRename(this.writer, objectReference, newName, fixupExpressions);
				this.WriteEndCommand(false, dictionary);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			base.SendExecuteAndReadResponse(true, true);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x000304AC File Offset: 0x0002E6AC
		private static void WriteRename(XmlWriter output, IDictionary objectReference, string name, FixUpExpressions fixupExpressions)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (objectReference == null)
			{
				throw new ArgumentNullException("objectReference");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name");
			}
			output.WriteStartElement("Rename", "http://schemas.microsoft.com/analysisservices/2003/engine");
			switch (fixupExpressions)
			{
			case FixUpExpressions.Enabled:
				output.WriteAttributeString("FixUpExpressions", XmlConvert.ToString(true));
				break;
			case FixUpExpressions.Disabled:
				output.WriteAttributeString("FixUpExpressions", XmlConvert.ToString(false));
				break;
			}
			output.WriteStartElement("ObjectReference");
			foreach (object obj in objectReference)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				output.WriteStartElement(dictionaryEntry.Key.ToString());
				output.WriteString(dictionaryEntry.Value.ToString());
				output.WriteEndElement();
			}
			output.WriteEndElement();
			output.WriteStartElement("Name");
			output.WriteString(name);
			output.WriteEndElement();
			output.WriteEndElement();
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x000305CC File Offset: 0x0002E7CC
		internal void Synchronize(string databaseID, string connectionString, SynchronizeSecurity synchronizeSecurity, bool applyCompression)
		{
			base.CheckConnection();
			databaseID = Utils.Trim(databaseID);
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				this.writer.WriteStartElement("Synchronize", "http://schemas.microsoft.com/analysisservices/2003/engine");
				this.writer.WriteStartElement("Source");
				this.writer.WriteElementString("ConnectionString", connectionString);
				this.writer.WriteStartElement("Object");
				this.writer.WriteElementString("DatabaseID", databaseID);
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				if (synchronizeSecurity != SynchronizeSecurity.SkipMembership)
				{
					this.writer.WriteElementString("SynchronizeSecurity", synchronizeSecurity.ToString());
				}
				if (applyCompression)
				{
					this.writer.WriteElementString("ApplyCompression", XmlConvert.ToString(applyCompression));
				}
				this.writer.WriteEndElement();
				base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, dictionary, null);
				base.EndMessage();
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			this.SendExecuteAndReadResponse(null, true, true);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x000306F0 File Offset: 0x0002E8F0
		internal XmlaResultCollection Execute(string command)
		{
			base.CheckConnection();
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				base.WriteCommandText(command);
				base.WriteEndCommand(base.IsConnected ? base.ConnectionInfo.ExtendedProperties : null, dictionary, null);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			return base.SendExecuteAndReadResponse(false, false);
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00030788 File Offset: 0x0002E988
		internal XmlaResultCollection Execute(StringCollection commands, bool transactional, bool parallel, bool processAffected, bool skipVolatileObjects)
		{
			base.CheckConnection();
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				base.WriteStartBatch(transactional, processAffected, skipVolatileObjects);
				if (parallel)
				{
					this.writer.WriteStartElement("Parallel", "http://schemas.microsoft.com/analysisservices/2003/engine");
				}
				int i = 0;
				int count = commands.Count;
				while (i < count)
				{
					base.WriteCommandText(commands[i]);
					i++;
				}
				if (parallel)
				{
					this.writer.WriteEndElement();
				}
				base.WriteEndBatch();
				this.WriteEndCommand(false, dictionary);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			return base.SendExecuteAndReadResponse(false, false);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00030858 File Offset: 0x0002EA58
		public XmlaResultCollection Execute(string command, ImpactDetailCollection impactResult)
		{
			try
			{
				base.StartMessage("SOAPAction: \"urn:schemas-microsoft-com:xml-analysis:Execute\"");
				IDictionary dictionary = null;
				base.WriteStartCommand(ref dictionary);
				base.WriteCommandText(command);
				this.WriteEndCommand(impactResult != null, dictionary);
				base.EndMessage();
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			return this.SendExecuteAndReadResponse(impactResult, false, false);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x000308D8 File Offset: 0x0002EAD8
		internal void CopyXmlaWarnings(XmlaResultCollection xmlaResults, XmlaWarningCollection xmlaWarnings)
		{
			if (xmlaResults == null || xmlaWarnings == null)
			{
				return;
			}
			foreach (object obj in ((IEnumerable)xmlaResults))
			{
				foreach (object obj2 in ((IEnumerable)((XmlaResult)obj).Messages))
				{
					XmlaMessage xmlaMessage = (XmlaMessage)obj2;
					if (xmlaMessage is XmlaWarning)
					{
						xmlaWarnings.Add((XmlaWarning)xmlaMessage);
					}
				}
			}
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00030980 File Offset: 0x0002EB80
		internal XmlaResultCollection SendExecuteAndReadResponse(ImpactDetailCollection impacts, bool expectEmptyResults, bool throwIfError)
		{
			XmlaResultCollection xmlaResultCollection = new XmlaResultCollection();
			if (this.captureXml)
			{
				base.EndRequest();
				return null;
			}
			if (impacts == null)
			{
				xmlaResultCollection = base.SendExecuteAndReadResponse(true, false);
				if (xmlaResultCollection.ContainsErrors && throwIfError)
				{
					throw new OperationException(xmlaResultCollection);
				}
				return xmlaResultCollection;
			}
			else
			{
				base.SendMessage(true, false, false);
				XmlaResult xmlaResult = new XmlaResult();
				xmlaResultCollection.Add(xmlaResult);
				try
				{
					if (!XmlaClient.CheckForError(this.reader, xmlaResult, false))
					{
						this.reader.ReadStartElement("ExecuteResponse", "urn:schemas-microsoft-com:xml-analysis");
						if (this.reader.IsStartElement("return") || this.reader.IsStartElement("return", "urn:schemas-microsoft-com:xml-analysis"))
						{
							if (this.reader.IsStartElement("return"))
							{
								this.reader.ReadStartElement("return");
							}
							else
							{
								this.reader.ReadStartElement("return", "urn:schemas-microsoft-com:xml-analysis");
							}
							if (this.reader.IsStartElement("results"))
							{
								if (this.reader.IsEmptyElement)
								{
									this.reader.Skip();
								}
								else
								{
									this.reader.ReadStartElement("results");
									while (this.reader.IsStartElement())
									{
										if (this.reader.IsStartElement("root", "urn:schemas-microsoft-com:xml-analysis:empty"))
										{
											if (this.reader.IsEmptyElement)
											{
												this.reader.Skip();
											}
											else
											{
												this.reader.ReadStartElement("root", "urn:schemas-microsoft-com:xml-analysis:empty");
												XmlaClient.CheckForError(this.reader, xmlaResult, false);
												this.reader.ReadEndElement();
											}
										}
										else if (!XmlaClient.CheckForError(this.reader, xmlaResult, false))
										{
											this.ReadImpact(this.reader, impacts);
										}
									}
									XmlaClient.CheckEndElement(this.reader, "results");
									this.reader.ReadEndElement();
								}
							}
							else
							{
								this.ReadImpact(this.reader, impacts);
							}
							XmlaClient.CheckEndElement(this.reader, "return");
							this.reader.ReadEndElement();
						}
						XmlaClient.CheckEndElement(this.reader, "ExecuteResponse");
						this.reader.ReadEndElement();
					}
					XmlaClient.CheckEndElement(this.reader, "Body");
					this.reader.ReadEndElement();
					XmlaClient.CheckEndElement(this.reader, "Envelope");
					this.reader.ReadEndElement();
					base.EndReceival();
				}
				catch (XmlException ex)
				{
					base.EndReceival();
					throw new ResponseFormatException(XmlaSR.UnknownServerResponseFormat, ex);
				}
				catch (IOException ex2)
				{
					base.CloseAll();
					throw new ConnectionException(XmlaSR.ConnectionBroken, ex2);
				}
				catch
				{
					base.CloseAll();
					throw;
				}
				if (xmlaResultCollection.ContainsErrors && throwIfError)
				{
					throw new OperationException(xmlaResultCollection);
				}
				return xmlaResultCollection;
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00030C64 File Offset: 0x0002EE64
		internal XmlaResultCollection SendMessageSkipResults()
		{
			return base.SendExecuteAndReadResponse(true, false);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00030C70 File Offset: 0x0002EE70
		internal void WriteEndCommand(bool analyzeImpact, IDictionary commandProperties)
		{
			base.CheckConnection();
			try
			{
				if (!this.captureXml)
				{
					if (!analyzeImpact)
					{
						base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, commandProperties, null);
					}
					else
					{
						if (commandProperties == null)
						{
							commandProperties = new ListDictionary();
						}
						commandProperties.Add("ImpactAnalysis", 1);
						base.WriteEndCommand(base.ConnectionInfo.ExtendedProperties, commandProperties, null);
					}
				}
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00030D0C File Offset: 0x0002EF0C
		internal XmlReader SendRequest(string action, Stream requestStream)
		{
			if (requestStream == null)
			{
				throw new ArgumentNullException("requestStream");
			}
			if (!requestStream.CanRead)
			{
				throw new ArgumentException(XmlaSR.XmlaClient_SendRequest_RequestStreamCannotBeRead);
			}
			base.StartRequest(action);
			try
			{
				char[] array = new char[4096];
				StreamReader streamReader = new StreamReader(requestStream, true);
				int num;
				while ((num = streamReader.Read(array, 0, 4096)) > 0)
				{
					this.writer.WriteRaw(array, 0, num);
				}
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			return base.EndRequest();
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00030DB8 File Offset: 0x0002EFB8
		internal XmlReader SendRequest(string action, TextReader request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			base.StartRequest(action);
			try
			{
				char[] array = new char[4096];
				int num;
				while ((num = request.Read(array, 0, 4096)) > 0)
				{
					this.writer.WriteRaw(array, 0, num);
				}
			}
			catch (IOException ex)
			{
				base.CloseAll();
				throw new ConnectionException(XmlaSR.ConnectionBroken, ex);
			}
			catch
			{
				base.HandleMessageCreationException();
				throw;
			}
			return base.EndRequest();
		}

		// Token: 0x040007AE RID: 1966
		internal const int MaxParallelDefault = -1;

		// Token: 0x040007AF RID: 1967
		internal const string ObjectElement = "Object";

		// Token: 0x040007B0 RID: 1968
		internal const string ParentObjectElement = "ParentObject";

		// Token: 0x040007B1 RID: 1969
		internal const string DeleteElement = "Delete";

		// Token: 0x040007B2 RID: 1970
		internal const string IgnoreFailuresAttribute = "IgnoreFailures";

		// Token: 0x040007B3 RID: 1971
		internal const bool IgnoreFailuresDefault = false;

		// Token: 0x040007B4 RID: 1972
		internal const string SubscribeElement = "Subscribe";

		// Token: 0x040007B5 RID: 1973
		internal const string SubscriptionIdElement = "SubscriptionId";

		// Token: 0x040007B6 RID: 1974
		internal const string UnsubscribeElement = "Unsubscribe";

		// Token: 0x040007B7 RID: 1975
		internal const string TraceIdElement = "TraceID";

		// Token: 0x040007B8 RID: 1976
		private Server owner;

		// Token: 0x020001A2 RID: 418
		internal static class Defaults
		{
			// Token: 0x04001051 RID: 4177
			public const WriteBackTableCreation Process_WriteBackTableCreation = WriteBackTableCreation.UseExisting;

			// Token: 0x04001052 RID: 4178
			public const bool Backup_BackupRemotePartitions = false;

			// Token: 0x04001053 RID: 4179
			public const bool Backup_ApplyCompression = true;

			// Token: 0x04001054 RID: 4180
			public const bool Backup_AllowOverwrite = false;

			// Token: 0x04001055 RID: 4181
			public const RestoreSecurity Restore_Security = RestoreSecurity.CopyAll;

			// Token: 0x04001056 RID: 4182
			public const bool Restore_AllowOverwrite = false;

			// Token: 0x04001057 RID: 4183
			public const bool Restore_IgnoreIncompatibilities = false;

			// Token: 0x04001058 RID: 4184
			public const bool Restore_ForceRestore = false;

			// Token: 0x04001059 RID: 4185
			public const RestoreDataSourceType Restore_DataSourceType = RestoreDataSourceType.Remote;

			// Token: 0x0400105A RID: 4186
			public const SynchronizeSecurity Synchronize_SynchronizeSecurity = SynchronizeSecurity.SkipMembership;

			// Token: 0x0400105B RID: 4187
			public const bool Synchronize_ApplyCompression = false;

			// Token: 0x0400105C RID: 4188
			public const ExportLayout Export_ExportLayout = ExportLayout.Delta;

			// Token: 0x0400105D RID: 4189
			public const ExportType Export_ExportType = ExportType.Full;
		}

		// Token: 0x020001A3 RID: 419
		internal static class Xml
		{
			// Token: 0x0400105E RID: 4190
			internal const string DiscoverXmlMetadata = "DISCOVER_XML_METADATA";

			// Token: 0x0400105F RID: 4191
			internal const string DiscoverDBSchemaCatalogs = "DBSCHEMA_CATALOGS";

			// Token: 0x04001060 RID: 4192
			internal const string DesignAggregations_DesignAggregationsElement = "DesignAggregations";

			// Token: 0x04001061 RID: 4193
			internal const string DesignAggregations_TimeElement = "Time";

			// Token: 0x04001062 RID: 4194
			internal const string DesignAggregations_StepsElement = "Steps";

			// Token: 0x04001063 RID: 4195
			internal const string DesignAggregations_OptimizationElement = "Optimization";

			// Token: 0x04001064 RID: 4196
			internal const string DesignAggregations_StorageElement = "Storage";

			// Token: 0x04001065 RID: 4197
			internal const string DesignAggregations_MaterializeElement = "Materialize";

			// Token: 0x04001066 RID: 4198
			internal const string DesignAggregations_QueriesElement = "Queries";

			// Token: 0x04001067 RID: 4199
			internal const string DesignAggregations_QueryElement = "Query";

			// Token: 0x04001068 RID: 4200
			internal const string DesignAggregations_AggregationsElement = "Aggregations";

			// Token: 0x04001069 RID: 4201
			internal const string DesignAggregations_LastStepElement = "LastStep";

			// Token: 0x0400106A RID: 4202
			internal const string NotifyTableChange_NotifyTableChangeElement = "NotifyTableChange";

			// Token: 0x0400106B RID: 4203
			internal const string NotifyTableChange_TableNotificationsElement = "TableNotifications";

			// Token: 0x0400106C RID: 4204
			internal const string NotifyTableChange_TableNotificationElement = "TableNotification";

			// Token: 0x0400106D RID: 4205
			internal const string TableNotification_DbTableNameElement = "DbTableName";

			// Token: 0x0400106E RID: 4206
			internal const string TableNotification_DbSchemaNameElement = "DbSchemaName";

			// Token: 0x0400106F RID: 4207
			internal const string Parallel_Element = "Parallel";

			// Token: 0x04001070 RID: 4208
			public const string Backup_BackupElement = "Backup";

			// Token: 0x04001071 RID: 4209
			public const string Backup_FileElement = "File";

			// Token: 0x04001072 RID: 4210
			public const string Backup_LocationsElement = "Locations";

			// Token: 0x04001073 RID: 4211
			public const string Backup_LocationElement = "Location";

			// Token: 0x04001074 RID: 4212
			public const string Backup_LocationFileElement = "File";

			// Token: 0x04001075 RID: 4213
			public const string Backup_LocationDataSourceIDElement = "DataSourceID";

			// Token: 0x04001076 RID: 4214
			public const string Backup_BackupRemotePartitionsElement = "BackupRemotePartitions";

			// Token: 0x04001077 RID: 4215
			public const string Backup_ApplyCompressionElement = "ApplyCompression";

			// Token: 0x04001078 RID: 4216
			public const string Backup_AllowOverwriteElement = "AllowOverwrite";

			// Token: 0x04001079 RID: 4217
			public const string Backup_PasswordElement = "Password";

			// Token: 0x0400107A RID: 4218
			public const string Restore_RestoreElement = "Restore";

			// Token: 0x0400107B RID: 4219
			public const string Restore_FileElement = "File";

			// Token: 0x0400107C RID: 4220
			public const string Restore_DatabaseNameElement = "DatabaseName";

			// Token: 0x0400107D RID: 4221
			public const string Restore_DatabaseIDElement = "DatabaseID";

			// Token: 0x0400107E RID: 4222
			public const string Restore_SecurityElement = "Security";

			// Token: 0x0400107F RID: 4223
			public const string Restore_AllowOverwriteElement = "AllowOverwrite";

			// Token: 0x04001080 RID: 4224
			public const string Restore_LocationsElement = "Locations";

			// Token: 0x04001081 RID: 4225
			public const string Restore_LocationElement = "Location";

			// Token: 0x04001082 RID: 4226
			public const string Restore_LocationFileElement = "File";

			// Token: 0x04001083 RID: 4227
			public const string Restore_LocationDataSourceIDElement = "DataSourceID";

			// Token: 0x04001084 RID: 4228
			public const string Restore_LocationDataSourceTypeElement = "DataSourceType";

			// Token: 0x04001085 RID: 4229
			public const string Restore_LocationConnectionStringElement = "ConnectionString";

			// Token: 0x04001086 RID: 4230
			public const string Restore_FoldersElement = "Folders";

			// Token: 0x04001087 RID: 4231
			public const string Restore_FolderElement = "Folder";

			// Token: 0x04001088 RID: 4232
			public const string Restore_FolderOriginalElement = "Original";

			// Token: 0x04001089 RID: 4233
			public const string Restore_FolderNewElement = "New";

			// Token: 0x0400108A RID: 4234
			public const string Restore_PasswordElement = "Password";

			// Token: 0x0400108B RID: 4235
			public const string Restore_DbStorageLocationElement = "DbStorageLocation";

			// Token: 0x0400108C RID: 4236
			public const string Restore_IgnoreIncompatibilities = "IgnoreIncompatibilities";

			// Token: 0x0400108D RID: 4237
			public const string Restore_ForceRestore = "ForceRestore";

			// Token: 0x0400108E RID: 4238
			public const string Process_ProcessElement = "Process";

			// Token: 0x0400108F RID: 4239
			public const string Process_TypeElement = "Type";

			// Token: 0x04001090 RID: 4240
			public const string Process_WriteBackTableCreationElement = "WriteBackTableCreation";

			// Token: 0x04001091 RID: 4241
			internal const string BeginTransactionElement = "BeginTransaction";

			// Token: 0x04001092 RID: 4242
			internal const string CommitTransactionElement = "CommitTransaction";

			// Token: 0x04001093 RID: 4243
			internal const string RollbackTransactionElement = "RollbackTransaction";

			// Token: 0x04001094 RID: 4244
			internal const string Attach_AttachElement = "Attach";

			// Token: 0x04001095 RID: 4245
			internal const string Attach_FolderElement = "Folder";

			// Token: 0x04001096 RID: 4246
			internal const string Attach_ReadWriteModeElement = "ReadWriteMode";

			// Token: 0x04001097 RID: 4247
			internal const string Attach_PasswordElement = "Password";

			// Token: 0x04001098 RID: 4248
			internal const string Detach_DetachElement = "Detach";

			// Token: 0x04001099 RID: 4249
			internal const string Detach_PasswordElement = "Password";

			// Token: 0x0400109A RID: 4250
			internal const string Rename_RenameElement = "Rename";

			// Token: 0x0400109B RID: 4251
			internal const string Rename_FixUpExpressionsAttribute = "FixUpExpressions";

			// Token: 0x0400109C RID: 4252
			internal const string Rename_DatabaseIDElement = "DatabaseID";

			// Token: 0x0400109D RID: 4253
			internal const string Rename_TableIDElement = "TableID";

			// Token: 0x0400109E RID: 4254
			internal const string Rename_ColumnIDElement = "ColumnID";

			// Token: 0x0400109F RID: 4255
			internal const string Rename_MeasureNameElement = "ScriptMeasureName";

			// Token: 0x040010A0 RID: 4256
			internal const string Rename_NameElement = "Name";

			// Token: 0x040010A1 RID: 4257
			internal const string Synchronize_SynchronizeElement = "Synchronize";

			// Token: 0x040010A2 RID: 4258
			internal const string Synchronize_SourceElement = "Source";

			// Token: 0x040010A3 RID: 4259
			internal const string Synchronize_ConnectionStringElement = "ConnectionString";

			// Token: 0x040010A4 RID: 4260
			internal const string Synchronize_DatabaseIDElement = "DatabaseID";

			// Token: 0x040010A5 RID: 4261
			internal const string Synchronize_SynchronizeSecurityElement = "SynchronizeSecurity";

			// Token: 0x040010A6 RID: 4262
			internal const string Synchronize_ApplyCompressionElement = "ApplyCompression";

			// Token: 0x040010A7 RID: 4263
			internal const string Export_ExportElement = "Export";

			// Token: 0x040010A8 RID: 4264
			internal const string Export_DatabaseIDElement = "DatabaseID";

			// Token: 0x040010A9 RID: 4265
			internal const string Export_LayoutElement = "Layout";

			// Token: 0x040010AA RID: 4266
			internal const string Export_TypeElement = "Type";

			// Token: 0x040010AB RID: 4267
			internal const string ObjectReferenceElement = "ObjectReference";

			// Token: 0x040010AC RID: 4268
			internal const string DbSchemaCatalog_CatalogName = "CATALOG_NAME";

			// Token: 0x040010AD RID: 4269
			internal const string DbSchemaCatalog_DatabaseId = "DATABASE_ID";
		}
	}
}
