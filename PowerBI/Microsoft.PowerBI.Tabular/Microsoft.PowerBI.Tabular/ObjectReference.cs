using System;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	public sealed class ObjectReference : IObjectReference
	{
		// Token: 0x0600114D RID: 4429 RVA: 0x0007C11F File Offset: 0x0007A31F
		public static ObjectReference GetObjectReference(IMajorObject obj)
		{
			if (obj != null)
			{
				return obj.ObjectReference;
			}
			return null;
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0007C12C File Offset: 0x0007A32C
		public static IMajorObject ResolveObjectReference(Database database, ObjectReference objectReference)
		{
			if (objectReference == null)
			{
				return null;
			}
			return objectReference.ResolveReference(database);
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0007C13A File Offset: 0x0007A33A
		public static IMajorObject ResolveObjectReference(Server server, ObjectReference objectReference)
		{
			if (objectReference == null)
			{
				return null;
			}
			return objectReference.ResolveReference(server);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0007C148 File Offset: 0x0007A348
		internal static object ResolveObjectReference(Server server, ObjectReference objectReference, bool forceLoad)
		{
			if (objectReference == null)
			{
				return null;
			}
			return objectReference.ResolveGenericReference(server, forceLoad);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0007C158 File Offset: 0x0007A358
		public static ObjectReference Deserialize(XmlReader xmlReader)
		{
			if (xmlReader == null)
			{
				throw new ArgumentNullException("xmlReader");
			}
			if (!xmlReader.IsStartElement("Object", "http://schemas.microsoft.com/analysisservices/2003/engine") && !xmlReader.IsStartElement("ParentObject", "http://schemas.microsoft.com/analysisservices/2003/engine"))
			{
				throw Utils.CreateSerializationException(xmlReader);
			}
			if (string.Compare(xmlReader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance"), bool.TrueString, true, CultureInfo.InvariantCulture) == 0)
			{
				xmlReader.Skip();
				return null;
			}
			ObjectReference objectReference = new ObjectReference();
			if (xmlReader.IsEmptyElement)
			{
				xmlReader.Skip();
			}
			else
			{
				xmlReader.ReadStartElement();
				objectReference.ReadContent(xmlReader);
				xmlReader.ReadEndElement();
			}
			return objectReference;
		}

		// Token: 0x06001152 RID: 4434 RVA: 0x0007C1F4 File Offset: 0x0007A3F4
		public static ObjectReference Deserialize(string xmlFragment, bool xmlFragmentIsComplete)
		{
			if (xmlFragment == null)
			{
				throw new ArgumentNullException("xmlFragment");
			}
			if (xmlFragmentIsComplete)
			{
				if (xmlFragment.Length == 0)
				{
					throw new ArgumentException(SR.Serialization_EmptyXmlString, "xmlFragment");
				}
			}
			else
			{
				xmlFragment = "<Object xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">" + xmlFragment + "</Object>";
			}
			return ObjectReference.Deserialize(new XmlTextReader(xmlFragment, XmlNodeType.Element, null)
			{
				DtdProcessing = DtdProcessing.Prohibit
			});
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001153 RID: 4435 RVA: 0x0007C250 File Offset: 0x0007A450
		[XmlIgnore]
		public bool IsValid
		{
			get
			{
				int num = 0;
				if (this.assemblyID != null)
				{
					num |= 1;
				}
				if (this.roleID != null)
				{
					num |= 2;
				}
				if (this.traceID != null)
				{
					num |= 4;
				}
				if (this.databaseID != null)
				{
					num |= 8;
				}
				int i = 0;
				int num2 = ObjectReference.AcceptedCombinations.Length - 1;
				while (i <= num2)
				{
					int num3 = i + (num2 - i >> 1);
					if (ObjectReference.AcceptedCombinations[num3] == num)
					{
						return true;
					}
					if (ObjectReference.AcceptedCombinations[num3] < num)
					{
						i = num3 + 1;
					}
					else
					{
						num2 = num3 - 1;
					}
				}
				return false;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001154 RID: 4436 RVA: 0x0007C2CA File Offset: 0x0007A4CA
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x0007C2D2 File Offset: 0x0007A4D2
		public string AssemblyID
		{
			get
			{
				return this.assemblyID;
			}
			set
			{
				this.assemblyID = Utils.Trim(value);
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001156 RID: 4438 RVA: 0x0007C2E0 File Offset: 0x0007A4E0
		// (set) Token: 0x06001157 RID: 4439 RVA: 0x0007C2E8 File Offset: 0x0007A4E8
		public string TraceID
		{
			get
			{
				return this.traceID;
			}
			set
			{
				this.traceID = Utils.Trim(value);
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001158 RID: 4440 RVA: 0x0007C2F6 File Offset: 0x0007A4F6
		// (set) Token: 0x06001159 RID: 4441 RVA: 0x0007C2FE File Offset: 0x0007A4FE
		public string RoleID
		{
			get
			{
				return this.roleID;
			}
			set
			{
				this.roleID = Utils.Trim(value);
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x0600115A RID: 4442 RVA: 0x0007C30C File Offset: 0x0007A50C
		// (set) Token: 0x0600115B RID: 4443 RVA: 0x0007C314 File Offset: 0x0007A514
		public string DatabaseID
		{
			get
			{
				return this.databaseID;
			}
			set
			{
				this.databaseID = Utils.Trim(value);
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0007C322 File Offset: 0x0007A522
		public IMajorObject ResolveReference(Database database)
		{
			return this.ResolveReference(database, true);
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x0007C32C File Offset: 0x0007A52C
		public IMajorObject ResolveReference(Database database, bool forceLoad)
		{
			if (database == null || this.databaseID == null || !this.IsValid)
			{
				return null;
			}
			if (!Utils.AreIDsEqual(this.databaseID, database.ID))
			{
				return null;
			}
			return database;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x0007C359 File Offset: 0x0007A559
		public IMajorObject ResolveReference(Server server)
		{
			return this.ResolveReference(server, true);
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x0007C364 File Offset: 0x0007A564
		public IMajorObject ResolveReference(Server server, bool forceLoad)
		{
			object obj = this.ResolveGenericReference(server, forceLoad);
			if (obj is IMajorObject)
			{
				return (IMajorObject)obj;
			}
			return null;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0007C38C File Offset: 0x0007A58C
		internal object ResolveGenericReference(Server server, bool forceLoad)
		{
			if (server == null || !this.IsValid)
			{
				return null;
			}
			if (this.databaseID != null)
			{
				if (server.IsLoaded || forceLoad)
				{
					return this.ResolveReference(server.Databases.Find(this.databaseID), forceLoad);
				}
				return null;
			}
			else if (this.roleID != null)
			{
				if (server.IsLoaded || forceLoad)
				{
					return server.Roles.Find(this.roleID);
				}
				return null;
			}
			else if (this.traceID != null)
			{
				if (server.IsLoaded || forceLoad)
				{
					return server.Traces.Find(this.traceID);
				}
				return null;
			}
			else
			{
				if (this.databaseName == null)
				{
					return server;
				}
				if (!server.IsLoaded && !forceLoad)
				{
					return null;
				}
				ObjectPath objectPath = new ObjectPath(ObjectType.Database, this.databaseName);
				if (this.tableName != null)
				{
					objectPath.Push(ObjectType.Table, this.tableName);
					if (this.partitionName != null)
					{
						objectPath.Push(ObjectType.Partition, this.partitionName);
					}
					else if (this.columnName != null)
					{
						objectPath.Push(ObjectType.Column, this.columnName);
						if (this.hasAttributeHierarchy)
						{
							objectPath.Push(ObjectType.AttributeHierarchy, string.Empty);
						}
					}
					else if (this.hierarchyName != null)
					{
						objectPath.Push(ObjectType.Hierarchy, this.hierarchyName);
					}
				}
				else if (this.relationshipName != null)
				{
					objectPath.Push(ObjectType.Relationship, this.relationshipName);
				}
				else if (this.perspectiveName != null)
				{
					objectPath.Push(ObjectType.Perspective, this.perspectiveName);
				}
				else if (this.roleName != null)
				{
					objectPath.Push(ObjectType.Role, this.roleName);
					if (this.roleMembershipName != null)
					{
						objectPath.Push(ObjectType.RoleMembership, this.roleMembershipName);
					}
					else if (this.tablePermissionName != null)
					{
						objectPath.Push(ObjectType.TablePermission, this.tablePermissionName);
						if (this.columnPermissionName != null)
						{
							objectPath.Push(ObjectType.ColumnPermission, this.columnPermissionName);
						}
					}
				}
				Database database = server.Databases.FindByName(this.databaseName);
				if (database == null)
				{
					return null;
				}
				if (this.modelName == null)
				{
					return database;
				}
				if (!database.IsModelLoaded() && !forceLoad)
				{
					return null;
				}
				return ObjectTreeHelper.LocateObjectByPath(objectPath, database.Model);
			}
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0007C590 File Offset: 0x0007A790
		public string Serialize()
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlTextWriter xmlTextWriter = null;
			string text;
			try
			{
				xmlTextWriter = new XmlTextWriter(stringWriter);
				xmlTextWriter.Formatting = Formatting.Indented;
				xmlTextWriter.Indentation = 2;
				this.Serialize(xmlTextWriter);
				xmlTextWriter.Flush();
				text = stringWriter.ToString();
			}
			finally
			{
				if (xmlTextWriter != null)
				{
					xmlTextWriter.Close();
				}
				stringWriter.Close();
			}
			return text;
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0007C5F8 File Offset: 0x0007A7F8
		public void Serialize(XmlWriter xmlWriter)
		{
			if (xmlWriter == null)
			{
				throw new ArgumentNullException("xmlWriter");
			}
			xmlWriter.WriteStartElement("Object", "http://schemas.microsoft.com/analysisservices/2003/engine");
			this.WriteContent(xmlWriter);
			xmlWriter.WriteEndElement();
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0007C628 File Offset: 0x0007A828
		public void WriteContent(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (this.assemblyID != null)
			{
				writer.WriteElementString("AssemblyID", this.assemblyID);
			}
			if (this.roleID != null)
			{
				writer.WriteElementString("RoleID", this.roleID);
			}
			if (this.traceID != null)
			{
				writer.WriteElementString("TraceID", this.traceID);
			}
			if (this.databaseID != null)
			{
				writer.WriteElementString("DatabaseID", this.databaseID);
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0007C6A8 File Offset: 0x0007A8A8
		public void ReadContent(XmlReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException();
			}
			while (reader.IsStartElement())
			{
				if (reader.NamespaceURI == "http://schemas.microsoft.com/analysisservices/2003/engine")
				{
					string name = reader.Name;
					if (name != null)
					{
						switch (name.Length)
						{
						case 4:
							if (!(name == "Role"))
							{
								return;
							}
							this.roleID = reader.ReadElementString();
							continue;
						case 5:
						{
							char c = name[0];
							if (c != 'M')
							{
								if (c != 'T')
								{
									return;
								}
								if (!(name == "Table"))
								{
									return;
								}
								this.tableName = reader.ReadElementString();
								continue;
							}
							else
							{
								if (!(name == "Model"))
								{
									return;
								}
								this.modelName = reader.ReadElementString();
								continue;
							}
							break;
						}
						case 6:
						{
							char c = name[0];
							if (c != 'C')
							{
								if (c != 'R')
								{
									return;
								}
								if (!(name == "RoleID"))
								{
									return;
								}
								this.RoleID = reader.ReadElementString();
								continue;
							}
							else if (!(name == "CubeID"))
							{
								if (!(name == "Column"))
								{
									return;
								}
								this.columnName = reader.ReadElementString();
								continue;
							}
							break;
						}
						case 7:
							if (!(name == "TraceID"))
							{
								return;
							}
							this.TraceID = reader.ReadElementString();
							continue;
						case 8:
						{
							char c = name[0];
							if (c != 'D')
							{
								if (c != 'S')
								{
									return;
								}
								if (!(name == "ServerID"))
								{
									return;
								}
								reader.Skip();
								continue;
							}
							else
							{
								if (!(name == "Database"))
								{
									return;
								}
								this.databaseName = reader.ReadElementString();
								continue;
							}
							break;
						}
						case 9:
						{
							char c = name[0];
							if (c != 'H')
							{
								if (c != 'P')
								{
									return;
								}
								if (!(name == "Partition"))
								{
									return;
								}
								this.partitionName = reader.ReadElementString();
								continue;
							}
							else
							{
								if (!(name == "Hierarchy"))
								{
									return;
								}
								this.hierarchyName = reader.ReadElementString();
								continue;
							}
							break;
						}
						case 10:
						{
							char c = name[0];
							if (c != 'A')
							{
								if (c != 'D')
								{
									return;
								}
								if (!(name == "DatabaseID"))
								{
									return;
								}
								this.DatabaseID = reader.ReadElementString();
								continue;
							}
							else
							{
								if (!(name == "AssemblyID"))
								{
									return;
								}
								this.AssemblyID = reader.ReadElementString();
								continue;
							}
							break;
						}
						case 11:
						{
							char c = name[1];
							switch (c)
							{
							case 'a':
								if (!(name == "PartitionID"))
								{
									return;
								}
								break;
							case 'b':
							case 'c':
								return;
							case 'd':
								if (!(name == "MdxScriptID"))
								{
									return;
								}
								break;
							case 'e':
								if (!(name == "Perspective"))
								{
									return;
								}
								this.perspectiveName = reader.ReadElementString();
								continue;
							default:
								if (c != 'i')
								{
									return;
								}
								if (!(name == "DimensionID"))
								{
									return;
								}
								break;
							}
							break;
						}
						case 12:
						{
							char c = name[0];
							if (c != 'D')
							{
								if (c != 'R')
								{
									return;
								}
								if (!(name == "Relationship"))
								{
									return;
								}
								this.relationshipName = reader.ReadElementString();
								continue;
							}
							else if (!(name == "DataSourceID"))
							{
								return;
							}
							break;
						}
						case 13:
						{
							char c = name[0];
							if (c != 'M')
							{
								if (c != 'P')
								{
									return;
								}
								if (!(name == "PerspectiveID"))
								{
									return;
								}
							}
							else if (!(name == "MiningModelID"))
							{
								return;
							}
							break;
						}
						case 14:
						{
							char c = name[0];
							if (c != 'M')
							{
								if (c != 'R')
								{
									return;
								}
								if (!(name == "RoleMembership"))
								{
									return;
								}
								this.roleMembershipName = reader.ReadElementString();
								continue;
							}
							else if (!(name == "MeasureGroupID"))
							{
								return;
							}
							break;
						}
						case 15:
							if (!(name == "TablePermission"))
							{
								return;
							}
							this.tablePermissionName = reader.ReadElementString();
							continue;
						case 16:
						{
							char c = name[0];
							if (c != 'C')
							{
								if (c != 'D')
								{
									return;
								}
								if (!(name == "DataSourceViewID"))
								{
									return;
								}
							}
							else if (!(name == "CubePermissionID"))
							{
								return;
							}
							break;
						}
						case 17:
							if (!(name == "MiningStructureID"))
							{
								return;
							}
							break;
						case 18:
							if (!(name == "AttributeHierarchy"))
							{
								return;
							}
							this.hasAttributeHierarchy = true;
							reader.ReadElementString();
							continue;
						case 19:
							if (!(name == "AggregationDesignID"))
							{
								return;
							}
							break;
						case 20:
							if (!(name == "DatabasePermissionID"))
							{
								return;
							}
							break;
						case 21:
							if (!(name == "DimensionPermissionID"))
							{
								return;
							}
							break;
						case 22:
							if (!(name == "DataSourcePermissionID"))
							{
								return;
							}
							break;
						case 23:
							if (!(name == "MiningModelPermissionID"))
							{
								return;
							}
							break;
						case 24:
						case 25:
						case 26:
							return;
						case 27:
							if (!(name == "MiningStructurePermissionID"))
							{
								return;
							}
							break;
						default:
							return;
						}
						reader.Skip();
						continue;
					}
					return;
				}
				return;
			}
		}

		// Token: 0x04000267 RID: 615
		private const string Tmp1 = "<Object xmlns=\"http://schemas.microsoft.com/analysisservices/2003/engine\">";

		// Token: 0x04000268 RID: 616
		private const string Tmp2 = "</Object>";

		// Token: 0x04000269 RID: 617
		private string assemblyID;

		// Token: 0x0400026A RID: 618
		private string roleID;

		// Token: 0x0400026B RID: 619
		private string traceID;

		// Token: 0x0400026C RID: 620
		private string databaseID;

		// Token: 0x0400026D RID: 621
		private string databaseName;

		// Token: 0x0400026E RID: 622
		private string modelName;

		// Token: 0x0400026F RID: 623
		private string tableName;

		// Token: 0x04000270 RID: 624
		private string partitionName;

		// Token: 0x04000271 RID: 625
		private string columnName;

		// Token: 0x04000272 RID: 626
		private bool hasAttributeHierarchy;

		// Token: 0x04000273 RID: 627
		private string hierarchyName;

		// Token: 0x04000274 RID: 628
		private string relationshipName;

		// Token: 0x04000275 RID: 629
		private string perspectiveName;

		// Token: 0x04000276 RID: 630
		private string roleName;

		// Token: 0x04000277 RID: 631
		private string roleMembershipName;

		// Token: 0x04000278 RID: 632
		private string tablePermissionName;

		// Token: 0x04000279 RID: 633
		private string columnPermissionName;

		// Token: 0x0400027A RID: 634
		private const int ServerMask = 0;

		// Token: 0x0400027B RID: 635
		private const int AssemblyMask = 1;

		// Token: 0x0400027C RID: 636
		private const int RoleMask = 2;

		// Token: 0x0400027D RID: 637
		private const int TraceMask = 4;

		// Token: 0x0400027E RID: 638
		private const int DatabaseMask = 8;

		// Token: 0x0400027F RID: 639
		private static int[] AcceptedCombinations = new int[] { 0, 1, 2, 4, 8, 9, 10 };

		// Token: 0x02000308 RID: 776
		internal class TabularObjectReferenceException : Exception
		{
		}
	}
}
