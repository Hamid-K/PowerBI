using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A7D RID: 2685
	public class StaticSql
	{
		// Token: 0x06005373 RID: 21363 RVA: 0x00153790 File Offset: 0x00151990
		public void LoadPackage(string fileName)
		{
			XmlReader xmlReader = this.GetXmlReader(fileName);
			this.LoadPackage(xmlReader);
		}

		// Token: 0x06005374 RID: 21364 RVA: 0x001537AC File Offset: 0x001519AC
		public void LoadPackage(XmlReader packageReader)
		{
			this._packages.Clear();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(packageReader);
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
			xmlNamespaceManager.AddNamespace("drdastaticsql", "http://schemas.microsoft.com/his/StaticSql/2012");
			XmlElement xmlElement = (XmlElement)xmlDocument.DocumentElement.SelectSingleNode("descendant::drdastaticsql:options", xmlNamespaceManager);
			if (xmlElement != null)
			{
				foreach (object obj in xmlElement.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					Type type = Type.GetType(this.GetQulifiedEnumTypeName(xmlAttribute.Name));
					if (type == null)
					{
						StaticSql.SetProperty(this._options, xmlAttribute.Name, xmlAttribute.Value);
					}
					else
					{
						StaticSql.SetProperty(this._options, xmlAttribute.Name, StaticSql.GetEnumValue(type, xmlAttribute.Value));
					}
				}
			}
			foreach (object obj2 in ((XmlElement)xmlDocument.DocumentElement.SelectSingleNode("descendant::drdastaticsql:packages", xmlNamespaceManager)).SelectNodes("descendant::drdastaticsql:package", xmlNamespaceManager))
			{
				XmlNode xmlNode = (XmlNode)obj2;
				if (xmlNode is XmlElement)
				{
					Package package = new Package();
					package.LoadFromXml((XmlElement)xmlNode, xmlNamespaceManager);
					this._packages.Add(package);
				}
			}
		}

		// Token: 0x06005375 RID: 21365 RVA: 0x00153944 File Offset: 0x00151B44
		public static object GetEnumValue(Type enumType, string valueInString)
		{
			if (enumType == null)
			{
				return valueInString;
			}
			int num = 0;
			if (int.TryParse(valueInString, out num))
			{
				return Enum.ToObject(enumType, num);
			}
			Array values = Enum.GetValues(enumType);
			for (int i = 0; i < values.Length; i++)
			{
				if (values.GetValue(i).ToString().Equals(valueInString, StringComparison.InvariantCultureIgnoreCase))
				{
					return values.GetValue(i);
				}
			}
			throw new Exception(string.Format("Enum value not found for name = {0}, type={1}", valueInString, enumType));
		}

		// Token: 0x06005376 RID: 21366 RVA: 0x001539B8 File Offset: 0x00151BB8
		public static bool SetProperty(object theObj, string propertyName, object val)
		{
			propertyName = char.ToUpperInvariant(propertyName[0]).ToString() + propertyName.Substring(1);
			PropertyInfo property = theObj.GetType().GetProperty(propertyName);
			bool flag = property != null;
			if (flag && val != null)
			{
				string text = val.ToString();
				if (text.Equals("true", StringComparison.InvariantCultureIgnoreCase) || text.Equals("false", StringComparison.InvariantCultureIgnoreCase))
				{
					property.SetValue(theObj, bool.Parse(text), null);
					return flag;
				}
				int num = -1;
				if (int.TryParse(text, out num))
				{
					property.SetValue(theObj, num, null);
					return flag;
				}
				property.SetValue(theObj, val, null);
			}
			return flag;
		}

		// Token: 0x06005377 RID: 21367 RVA: 0x00153A5C File Offset: 0x00151C5C
		public void SavePackage(Stream stream)
		{
			XmlWriter xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings
			{
				Indent = true,
				OmitXmlDeclaration = false,
				NewLineOnAttributes = false,
				CloseOutput = true,
				CheckCharacters = false
			});
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("hostIntegration.staticSql", "http://schemas.microsoft.com/his/StaticSql/2012");
			xmlWriter.WriteStartElement("options");
			string[] array = new string[this._options._optionsTable.Count];
			this._options._optionsTable.Keys.CopyTo(array, 0);
			Array.Sort<string>(array);
			foreach (string text in array)
			{
				object obj = this._options._optionsTable[text];
				if (obj is bool || obj is int || obj is string)
				{
					xmlWriter.WriteAttributeString(text, obj.ToString());
				}
				else
				{
					xmlWriter.WriteAttributeString(text, Enum.GetName(Type.GetType(this.GetQulifiedEnumTypeName(text)), obj));
				}
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteStartElement("packages");
			foreach (Package package in this._packages)
			{
				package.SaveToXml(xmlWriter);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			xmlWriter.Flush();
		}

		// Token: 0x06005378 RID: 21368 RVA: 0x00153BD4 File Offset: 0x00151DD4
		private string GetQulifiedEnumTypeName(string bndopt)
		{
			return "Microsoft.HostIntegration.StaticSqlUtil.Options" + char.ToUpperInvariant(bndopt[0]).ToString() + bndopt.Substring(1);
		}

		// Token: 0x06005379 RID: 21369 RVA: 0x00153C06 File Offset: 0x00151E06
		public void SavePackage(string fileName)
		{
			this.SavePackage(new FileStream(fileName, FileMode.Create));
		}

		// Token: 0x0600537A RID: 21370 RVA: 0x00153C18 File Offset: 0x00151E18
		public void LoadPackage(string fileName, PackageFormat format)
		{
			XmlReader xmlReader = this.GetXmlReader(fileName);
			this.LoadPackage(xmlReader, format);
		}

		// Token: 0x0600537B RID: 21371 RVA: 0x00153C38 File Offset: 0x00151E38
		public void LoadPackage(XmlReader packageReader, PackageFormat format)
		{
			if (format == PackageFormat.v90)
			{
				this.LoadPackage(packageReader);
				return;
			}
			this._packages.Clear();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			xmlDocument.Load(packageReader);
			XmlElement xmlElement = (XmlElement)xmlDocument.DocumentElement.SelectSingleNode("Options");
			if (xmlElement != null)
			{
				foreach (object obj in xmlElement.ChildNodes)
				{
					XmlElement xmlElement2 = ((XmlNode)obj) as XmlElement;
					if (xmlElement2 != null)
					{
						if (xmlElement2.Name.Equals("PKGDFTCC", StringComparison.CurrentCultureIgnoreCase))
						{
							using (IEnumerator enumerator2 = xmlElement2.ChildNodes.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									XmlElement xmlElement3 = (XmlElement)obj2;
									string displayNameForCodepoint = this.GetDisplayNameForCodepoint(xmlElement3.Name);
									if (string.IsNullOrEmpty(displayNameForCodepoint))
									{
										throw new Exception("Invalid bind option: " + xmlElement3.Name);
									}
									string innerText = xmlElement3.InnerText;
									BNDOPTCodePoint bndoptcodePoint = (BNDOPTCodePoint)StaticSql.GetEnumValue(typeof(BNDOPTCodePoint), xmlElement3.Name);
									if (bndoptcodePoint - BNDOPTCodePoint.CCSIDSBC <= 2)
									{
										StaticSql.SetProperty(this._options, displayNameForCodepoint, int.Parse(innerText));
									}
								}
								continue;
							}
						}
						this.SetOption(xmlElement2.Name, xmlElement2.InnerText);
					}
				}
			}
			foreach (object obj3 in xmlDocument.DocumentElement.SelectNodes("Package"))
			{
				XmlNode xmlNode = (XmlNode)obj3;
				if (xmlNode is XmlElement)
				{
					Package package = new Package();
					package.LoadFromXmlV8((XmlElement)xmlNode);
					if (!string.IsNullOrEmpty(package._isolationLevel))
					{
						this.SetOption(BNDOPTCodePoint.PKGISOLVL.ToString(), package._isolationLevel.ToUpperInvariant());
					}
					this._packages.Add(package);
				}
			}
		}

		// Token: 0x0600537C RID: 21372 RVA: 0x00153EA8 File Offset: 0x001520A8
		public void SetOption(string codePoint, string value)
		{
			string displayNameForCodepoint = this.GetDisplayNameForCodepoint(codePoint);
			if (string.IsNullOrEmpty(displayNameForCodepoint))
			{
				throw new Exception("Invalid bind option: " + codePoint);
			}
			BNDOPTCodePoint bndoptcodePoint = (BNDOPTCodePoint)StaticSql.GetEnumValue(typeof(BNDOPTCodePoint), codePoint);
			int num = -1;
			if (bndoptcodePoint <= BNDOPTCodePoint.CCSIDMBC)
			{
				if (bndoptcodePoint == BNDOPTCodePoint.TITLE || bndoptcodePoint == BNDOPTCodePoint.VRSNAM)
				{
					goto IL_08A8;
				}
				if (bndoptcodePoint - BNDOPTCodePoint.CCSIDSBC > 2)
				{
					goto IL_08B7;
				}
			}
			else if (bndoptcodePoint <= BNDOPTCodePoint.QRYBLKCTL)
			{
				if (bndoptcodePoint != BNDOPTCodePoint.DECPRC)
				{
					switch (bndoptcodePoint)
					{
					case BNDOPTCodePoint.RDBNAM:
					case BNDOPTCodePoint.DFTRDBCOL:
					case BNDOPTCodePoint.PKGRPLVRS:
					case BNDOPTCodePoint.PKGOWNID:
						goto IL_08A8;
					case (BNDOPTCodePoint)8465:
					case (BNDOPTCodePoint)8466:
					case (BNDOPTCodePoint)8467:
					case (BNDOPTCodePoint)8468:
					case (BNDOPTCodePoint)8469:
					case (BNDOPTCodePoint)8470:
					case (BNDOPTCodePoint)8471:
					case (BNDOPTCodePoint)8472:
					case (BNDOPTCodePoint)8473:
					case (BNDOPTCodePoint)8474:
					case (BNDOPTCodePoint)8479:
					case (BNDOPTCodePoint)8486:
					case (BNDOPTCodePoint)8487:
					case (BNDOPTCodePoint)8490:
					case (BNDOPTCodePoint)8491:
					case (BNDOPTCodePoint)8492:
					case (BNDOPTCodePoint)8494:
						goto IL_08B7;
					case BNDOPTCodePoint.BNDCHKEXS:
						if (value.Equals("BNDEXSOPT", StringComparison.InvariantCultureIgnoreCase))
						{
							StaticSql.SetProperty(this._options, displayNameForCodepoint, false);
							return;
						}
						if (value.Equals("BNDEXSRQR", StringComparison.InvariantCultureIgnoreCase))
						{
							StaticSql.SetProperty(this._options, displayNameForCodepoint, true);
							return;
						}
						throw new Exception("Invalid bind check option value: " + value);
					case BNDOPTCodePoint.PKGRPLOPT:
						if (value.Equals("PKGRPLNA", StringComparison.InvariantCultureIgnoreCase))
						{
							StaticSql.SetProperty(this._options, displayNameForCodepoint, false);
							return;
						}
						if (value.Equals("PKGRPLALW", StringComparison.InvariantCultureIgnoreCase))
						{
							StaticSql.SetProperty(this._options, displayNameForCodepoint, true);
							return;
						}
						throw new Exception("Invalid bind replace option value: " + value);
					case BNDOPTCodePoint.BNDCRTCTL:
					case BNDOPTCodePoint.STTSTRDEL:
					case BNDOPTCodePoint.STTDECDEL:
					case BNDOPTCodePoint.STTDATFMT:
					case BNDOPTCodePoint.STTTIMFMT:
					case BNDOPTCodePoint.PKGDFTCST:
					case BNDOPTCodePoint.RDBRLSOPT:
					case BNDOPTCodePoint.BNDEXPOPT:
					case BNDOPTCodePoint.QRYBLKCTL:
					{
						string displayNameForCodepoint2 = this.GetDisplayNameForCodepoint(value);
						if (string.IsNullOrEmpty(displayNameForCodepoint2))
						{
							throw new Exception("Failed to get code point for option value: " + displayNameForCodepoint2);
						}
						Type type = Type.GetType(this.GetQulifiedEnumTypeName(displayNameForCodepoint));
						if (type != null)
						{
							StaticSql.SetProperty(this._options, displayNameForCodepoint, StaticSql.GetEnumValue(type, displayNameForCodepoint2));
							return;
						}
						throw new Exception("Failed to retrieve option enum type: " + displayNameForCodepoint);
					}
					case BNDOPTCodePoint.PKGATHOPT:
						if (value.Equals("PKGATHRVK", StringComparison.InvariantCultureIgnoreCase))
						{
							StaticSql.SetProperty(this._options, displayNameForCodepoint, false);
							return;
						}
						if (value.Equals("PKGATHKP", StringComparison.InvariantCultureIgnoreCase))
						{
							StaticSql.SetProperty(this._options, displayNameForCodepoint, true);
							return;
						}
						throw new Exception("Invalid bind authorization option value: " + value);
					case BNDOPTCodePoint.PKGISOLVL:
					{
						string text = value.ToUpperInvariant();
						if (text != null)
						{
							uint num2 = <c98b5b6f-a76e-400d-a518-0c889a923ea0><PrivateImplementationDetails>.ComputeStringHash(text);
							OptionsPackageIsolationLevel optionsPackageIsolationLevel;
							if (num2 > 1492939328U)
							{
								if (num2 <= 2528990880U)
								{
									if (num2 != 1646644850U)
									{
										if (num2 != 1647189303U)
										{
											if (num2 != 2528990880U)
											{
												goto IL_043A;
											}
											if (!(text == "ISOLVLCHG"))
											{
												goto IL_043A;
											}
										}
										else
										{
											if (!(text == "ISOLVLNC"))
											{
												goto IL_043A;
											}
											goto IL_0432;
										}
									}
									else
									{
										if (!(text == "ISOLVLCS"))
										{
											goto IL_043A;
										}
										goto IL_042A;
									}
								}
								else if (num2 <= 3240250096U)
								{
									if (num2 != 2767900559U)
									{
										if (num2 != 3240250096U)
										{
											goto IL_043A;
										}
										if (!(text == "READUNCOMMITTED"))
										{
											goto IL_043A;
										}
									}
									else
									{
										if (!(text == "ISOLVLALL"))
										{
											goto IL_043A;
										}
										goto IL_041A;
									}
								}
								else if (num2 != 3949800934U)
								{
									if (num2 != 4280911421U)
									{
										goto IL_043A;
									}
									if (!(text == "CHANGE"))
									{
										goto IL_043A;
									}
								}
								else
								{
									if (!(text == "REPEATABLEREAD"))
									{
										goto IL_043A;
									}
									goto IL_041A;
								}
								optionsPackageIsolationLevel = OptionsPackageIsolationLevel.ReadUncommitted;
								goto IL_044B;
							}
							if (num2 <= 1119297926U)
							{
								if (num2 != 14865273U)
								{
									if (num2 != 795258455U)
									{
										if (num2 != 1119297926U)
										{
											goto IL_043A;
										}
										if (!(text == "CURSORSTABILITY"))
										{
											goto IL_043A;
										}
										goto IL_042A;
									}
									else
									{
										if (!(text == "NOCOMMIT"))
										{
											goto IL_043A;
										}
										goto IL_0432;
									}
								}
								else
								{
									if (!(text == "READCOMMITTED"))
									{
										goto IL_043A;
									}
									goto IL_042A;
								}
							}
							else
							{
								if (num2 != 1432457764U)
								{
									if (num2 != 1445583890U)
									{
										if (num2 != 1492939328U)
										{
											goto IL_043A;
										}
										if (!(text == "ISOLVLRR"))
										{
											goto IL_043A;
										}
									}
									else if (!(text == "SERIALIZABLE"))
									{
										goto IL_043A;
									}
									optionsPackageIsolationLevel = OptionsPackageIsolationLevel.Serializable;
									goto IL_044B;
								}
								if (!(text == "ALL"))
								{
									goto IL_043A;
								}
							}
							IL_041A:
							optionsPackageIsolationLevel = OptionsPackageIsolationLevel.RepeatableRead;
							goto IL_044B;
							IL_042A:
							optionsPackageIsolationLevel = OptionsPackageIsolationLevel.ReadCommitted;
							goto IL_044B;
							IL_0432:
							optionsPackageIsolationLevel = OptionsPackageIsolationLevel.NoCommit;
							IL_044B:
							StaticSql.SetProperty(this._options, displayNameForCodepoint, optionsPackageIsolationLevel);
							return;
						}
						IL_043A:
						throw new Exception("Invalid isolation level: " + value);
					}
					case BNDOPTCodePoint.DGRIOPRL:
						break;
					default:
						goto IL_08B7;
					}
				}
			}
			else if (bndoptcodePoint != BNDOPTCodePoint.PKGATHRUL)
			{
				if (bndoptcodePoint != BNDOPTCodePoint.PRPSTTKP)
				{
					goto IL_08B7;
				}
				if (int.TryParse(value, out num))
				{
					value = byte.Parse(value).ToString("X2");
				}
				string text = value.ToUpperInvariant().Trim();
				if (text != null)
				{
					if (text == "F0")
					{
						StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsKeepPreparedStatement.None);
						return;
					}
					if (text == "F1")
					{
						StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsKeepPreparedStatement.Commit);
						return;
					}
					if (text == "F2")
					{
						StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsKeepPreparedStatement.Rollback);
						return;
					}
					if (text == "F3")
					{
						StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsKeepPreparedStatement.All);
						return;
					}
				}
				throw new Exception("Invalid option value for PRPSTTKP: " + value);
			}
			else
			{
				if (!int.TryParse(value, out num))
				{
					string text = value.ToUpperInvariant().Trim();
					if (text != null)
					{
						uint num2 = <c98b5b6f-a76e-400d-a518-0c889a923ea0><PrivateImplementationDetails>.ComputeStringHash(text);
						if (num2 <= 2207888833U)
						{
							if (num2 <= 498076224U)
							{
								if (num2 != 27257576U)
								{
									if (num2 == 498076224U)
									{
										if (text == "DEFINER")
										{
											StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.Definer);
											return;
										}
									}
								}
								else if (text == "INVOKER_REVERT_TO_OWNER")
								{
									StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.InvokerRevertToOwner);
									return;
								}
							}
							else if (num2 != 2188130213U)
							{
								if (num2 == 2207888833U)
								{
									if (text == "DEFINER_REVERT_TO_OWNER")
									{
										StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.DefinerRevertToOwner);
										return;
									}
								}
							}
							else if (text == "REQUESTER")
							{
								StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.Requester);
								return;
							}
						}
						else if (num2 <= 2637269620U)
						{
							if (num2 != 2249835329U)
							{
								if (num2 == 2637269620U)
								{
									if (text == "OWNER")
									{
										StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.Owner);
										return;
									}
								}
							}
							else if (text == "INVOKER_REVERT_TO_REQUESTER")
							{
								StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.InvokerRevertToRequester);
								return;
							}
						}
						else if (num2 != 2937781837U)
						{
							if (num2 == 4183821168U)
							{
								if (text == "DEFINER_REVERT_TO_REQUESTER")
								{
									StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.DefinerRevertToRequester);
									return;
								}
							}
						}
						else if (text == "INVOKER")
						{
							StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.Invoker);
							return;
						}
					}
					throw new Exception("Invalid option value for PKGATHRUL:" + value);
				}
				switch (num)
				{
				case 0:
					StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.Owner);
					return;
				case 1:
					StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.Requester);
					return;
				case 2:
					StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.Invoker);
					return;
				case 3:
					StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.Definer);
					return;
				case 4:
					StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.InvokerRevertToRequester);
					return;
				case 5:
					StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.InvokerRevertToOwner);
					return;
				case 6:
					StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.DefinerRevertToRequester);
					return;
				case 7:
					StaticSql.SetProperty(this._options, displayNameForCodepoint, OptionsPackageExecuteAuthorization.DefinerRevertToOwner);
					return;
				default:
					throw new Exception("Invalid option value for PKGATHRUL:" + value);
				}
			}
			StaticSql.SetProperty(this._options, displayNameForCodepoint, int.Parse(value));
			return;
			IL_08A8:
			StaticSql.SetProperty(this._options, displayNameForCodepoint, value);
			return;
			IL_08B7:
			throw new Exception("Invalid bind option:" + bndoptcodePoint.ToString());
		}

		// Token: 0x0600537D RID: 21373 RVA: 0x00154788 File Offset: 0x00152988
		private string GetDisplayNameForCodepoint(string codepoint)
		{
			Array values = Enum.GetValues(typeof(BNDOPTCodePoint));
			for (int i = 0; i < values.Length; i++)
			{
				if (Enum.GetName(typeof(BNDOPTCodePoint), values.GetValue(i)).Equals(codepoint, StringComparison.InvariantCultureIgnoreCase))
				{
					return StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[(BNDOPTCodePoint)values.GetValue(i)].Item1;
				}
			}
			return string.Empty;
		}

		// Token: 0x0600537E RID: 21374 RVA: 0x001547F8 File Offset: 0x001529F8
		public void SavePackage(string fileName, PackageFormat format)
		{
			FileStream fileStream = new FileStream(fileName, FileMode.Create);
			this.SavePackage(fileStream, format);
			fileStream.Close();
		}

		// Token: 0x0600537F RID: 21375 RVA: 0x0015481C File Offset: 0x00152A1C
		public void SavePackage(Stream stream, PackageFormat format)
		{
			if (format == PackageFormat.v90)
			{
				this.SavePackage(stream);
				return;
			}
			XmlWriter xmlWriter = XmlWriter.Create(stream, new XmlWriterSettings
			{
				Indent = true,
				OmitXmlDeclaration = false,
				NewLineOnAttributes = false,
				CloseOutput = true,
				CheckCharacters = false
			});
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("Packages");
			xmlWriter.WriteStartElement("Options");
			string[] array = new string[this._options._optionsTable.Count];
			this._options._optionsTable.Keys.CopyTo(array, 0);
			Array.Sort<string>(array);
			foreach (string text in array)
			{
				object obj = this._options._optionsTable[text];
				BNDOPTCodePoint codepointFromDisplayName = this.GetCodepointFromDisplayName(text);
				if (codepointFromDisplayName == BNDOPTCodePoint.UNKNOWN)
				{
					throw new Exception("Invalid bnd option code point");
				}
				if (codepointFromDisplayName != BNDOPTCodePoint.CCSIDSBC && codepointFromDisplayName != BNDOPTCodePoint.CCSIDMBC && codepointFromDisplayName != BNDOPTCodePoint.CCSIDDBC)
				{
					string name = Enum.GetName(typeof(BNDOPTCodePoint), codepointFromDisplayName);
					xmlWriter.WriteStartElement(name);
					if (codepointFromDisplayName <= BNDOPTCodePoint.DECPRC)
					{
						if (codepointFromDisplayName != BNDOPTCodePoint.TITLE && codepointFromDisplayName != BNDOPTCodePoint.VRSNAM && codepointFromDisplayName != BNDOPTCodePoint.DECPRC)
						{
							goto IL_03E6;
						}
						goto IL_03D7;
					}
					else
					{
						switch (codepointFromDisplayName)
						{
						case BNDOPTCodePoint.RDBNAM:
						case BNDOPTCodePoint.DFTRDBCOL:
						case BNDOPTCodePoint.PKGRPLVRS:
						case BNDOPTCodePoint.DGRIOPRL:
						case BNDOPTCodePoint.PKGOWNID:
							goto IL_03D7;
						case (BNDOPTCodePoint)8465:
						case (BNDOPTCodePoint)8466:
						case (BNDOPTCodePoint)8467:
						case (BNDOPTCodePoint)8468:
						case (BNDOPTCodePoint)8469:
						case (BNDOPTCodePoint)8470:
						case (BNDOPTCodePoint)8471:
						case (BNDOPTCodePoint)8472:
						case (BNDOPTCodePoint)8473:
						case (BNDOPTCodePoint)8474:
						case (BNDOPTCodePoint)8479:
						case (BNDOPTCodePoint)8486:
						case (BNDOPTCodePoint)8487:
						case (BNDOPTCodePoint)8490:
						case (BNDOPTCodePoint)8491:
						case (BNDOPTCodePoint)8492:
						case (BNDOPTCodePoint)8494:
							goto IL_03E6;
						case BNDOPTCodePoint.BNDCHKEXS:
							if ((bool)obj)
							{
								xmlWriter.WriteString("BNDEXSRQR");
								goto IL_0403;
							}
							xmlWriter.WriteString("BNDEXSOPT");
							goto IL_0403;
						case BNDOPTCodePoint.PKGRPLOPT:
							if ((bool)obj)
							{
								xmlWriter.WriteString("PKGRPLALW");
								goto IL_0403;
							}
							xmlWriter.WriteString("PKGRPLNA");
							goto IL_0403;
						case BNDOPTCodePoint.BNDCRTCTL:
						case BNDOPTCodePoint.PKGISOLVL:
						case BNDOPTCodePoint.PKGDFTCST:
						case BNDOPTCodePoint.BNDEXPOPT:
						{
							using (List<Package>.Enumerator enumerator = this._packages.GetEnumerator())
							{
								while (enumerator.MoveNext())
								{
									Package package = enumerator.Current;
									xmlWriter.WriteString(this.GetCodepointInStringFromDisplayName(Enum.GetName(obj.GetType(), obj)));
								}
								goto IL_0403;
							}
							break;
						}
						case BNDOPTCodePoint.PKGATHOPT:
							if ((bool)obj)
							{
								xmlWriter.WriteString("PKGATHKP");
								goto IL_0403;
							}
							xmlWriter.WriteString("PKGATHRVK");
							goto IL_0403;
						case BNDOPTCodePoint.STTSTRDEL:
						case BNDOPTCodePoint.STTDECDEL:
						case BNDOPTCodePoint.STTDATFMT:
						case BNDOPTCodePoint.STTTIMFMT:
						case BNDOPTCodePoint.RDBRLSOPT:
						case BNDOPTCodePoint.QRYBLKCTL:
							break;
						default:
							if (codepointFromDisplayName == BNDOPTCodePoint.PKGATHRUL)
							{
								switch ((OptionsPackageExecuteAuthorization)obj)
								{
								case OptionsPackageExecuteAuthorization.Requester:
									xmlWriter.WriteString("REQUESTER");
									goto IL_0403;
								case OptionsPackageExecuteAuthorization.Owner:
									xmlWriter.WriteString("OWNER");
									goto IL_0403;
								case OptionsPackageExecuteAuthorization.InvokerRevertToRequester:
									xmlWriter.WriteString("INVOKER_REVERT_TO_REQUESTER");
									goto IL_0403;
								case OptionsPackageExecuteAuthorization.InvokerRevertToOwner:
									xmlWriter.WriteString("INVOKER_REVERT_TO_OWNER");
									goto IL_0403;
								case OptionsPackageExecuteAuthorization.DefinerRevertToRequester:
									xmlWriter.WriteString("DEFINER_REVERT_TO_REQUESTER");
									goto IL_0403;
								case OptionsPackageExecuteAuthorization.DefinerRevertToOwner:
									xmlWriter.WriteString("DEFINER_REVERT_TO_OWNER");
									goto IL_0403;
								}
								throw new Exception("Invalid option value for PKGATHRUL");
							}
							if (codepointFromDisplayName != BNDOPTCodePoint.PRPSTTKP)
							{
								goto IL_03E6;
							}
							switch ((OptionsKeepPreparedStatement)obj)
							{
							case OptionsKeepPreparedStatement.None:
								xmlWriter.WriteString("F0");
								goto IL_0403;
							case OptionsKeepPreparedStatement.Commit:
								xmlWriter.WriteString("F1");
								goto IL_0403;
							case OptionsKeepPreparedStatement.Rollback:
								xmlWriter.WriteString("F2");
								goto IL_0403;
							case OptionsKeepPreparedStatement.All:
								xmlWriter.WriteString("F3");
								goto IL_0403;
							default:
								throw new Exception("Invalid option value for PRPSTTKP");
							}
							break;
						}
						xmlWriter.WriteString(this.GetCodepointInStringFromDisplayName(Enum.GetName(obj.GetType(), obj)));
					}
					IL_0403:
					xmlWriter.WriteEndElement();
					goto IL_0409;
					IL_03D7:
					xmlWriter.WriteString(obj.ToString());
					goto IL_0403;
					IL_03E6:
					throw new Exception("Invalid bind option: " + codepointFromDisplayName.ToString());
				}
				IL_0409:;
			}
			xmlWriter.WriteStartElement("PKGDFTCC");
			foreach (string text2 in array)
			{
				object obj2 = this._options._optionsTable[text2];
				BNDOPTCodePoint codepointFromDisplayName2 = this.GetCodepointFromDisplayName(text2);
				if (codepointFromDisplayName2 == BNDOPTCodePoint.UNKNOWN)
				{
					throw new Exception("Invalid bind option code point");
				}
				if (codepointFromDisplayName2 == BNDOPTCodePoint.CCSIDSBC || codepointFromDisplayName2 == BNDOPTCodePoint.CCSIDMBC || codepointFromDisplayName2 == BNDOPTCodePoint.CCSIDDBC)
				{
					string name2 = Enum.GetName(typeof(BNDOPTCodePoint), codepointFromDisplayName2);
					xmlWriter.WriteStartElement(name2);
					if (codepointFromDisplayName2 - BNDOPTCodePoint.CCSIDSBC <= 2)
					{
						xmlWriter.WriteString(obj2.ToString());
					}
					xmlWriter.WriteEndElement();
				}
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
			foreach (Package package2 in this._packages)
			{
				package2.SaveToXmlV8(xmlWriter);
			}
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();
			xmlWriter.Flush();
		}

		// Token: 0x06005380 RID: 21376 RVA: 0x00154D64 File Offset: 0x00152F64
		private string GetOptionValueInStringFromCodepoint(string codepoint)
		{
			foreach (BNDOPTCodePoint bndoptcodePoint in StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS.Keys)
			{
				Tuple<string, string> tuple = StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[bndoptcodePoint];
				if (tuple.Item2.Equals(codepoint, StringComparison.InvariantCultureIgnoreCase))
				{
					return tuple.Item1;
				}
			}
			return string.Empty;
		}

		// Token: 0x06005381 RID: 21377 RVA: 0x00154DE0 File Offset: 0x00152FE0
		private BNDOPTCodePoint GetCodepointFromDisplayName(string displayName)
		{
			foreach (BNDOPTCodePoint bndoptcodePoint in StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS.Keys)
			{
				if (StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[bndoptcodePoint].Item1.Equals(displayName, StringComparison.InvariantCultureIgnoreCase))
				{
					return bndoptcodePoint;
				}
			}
			return BNDOPTCodePoint.UNKNOWN;
		}

		// Token: 0x06005382 RID: 21378 RVA: 0x00154E50 File Offset: 0x00153050
		private string GetCodepointInStringFromDisplayName(string displayName)
		{
			foreach (BNDOPTCodePoint bndoptcodePoint in StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS.Keys)
			{
				Tuple<string, string> tuple = StaticSqlConstants.CODEPOINT_XML_NAME_MAPPINGS[bndoptcodePoint];
				if (tuple.Item1.Equals(displayName, StringComparison.InvariantCultureIgnoreCase))
				{
					return tuple.Item2;
				}
			}
			return string.Empty;
		}

		// Token: 0x17001443 RID: 5187
		// (get) Token: 0x06005383 RID: 21379 RVA: 0x00154ECC File Offset: 0x001530CC
		public Options Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x17001444 RID: 5188
		// (get) Token: 0x06005384 RID: 21380 RVA: 0x00154ED4 File Offset: 0x001530D4
		public List<Package> Packages
		{
			get
			{
				return this._packages;
			}
		}

		// Token: 0x06005385 RID: 21381 RVA: 0x00154EDC File Offset: 0x001530DC
		public XmlDocument GetXmlDocument(PackageFormat packageFormat)
		{
			MemoryStream memoryStream = new MemoryStream();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			if (packageFormat == PackageFormat.v85)
			{
				this.SavePackage(memoryStream, PackageFormat.v85);
				memoryStream.Position = 0L;
				XmlReader xmlReader = XmlReader.Create(memoryStream, xmlReaderSettings);
				xmlDocument.Load(xmlReader);
			}
			else
			{
				this.SavePackage(memoryStream, PackageFormat.v90);
				memoryStream.Position = 0L;
				XmlReader xmlReader2 = XmlReader.Create(memoryStream, xmlReaderSettings);
				xmlDocument.Load(xmlReader2);
			}
			return xmlDocument;
		}

		// Token: 0x06005386 RID: 21382 RVA: 0x00154F58 File Offset: 0x00153158
		private XmlReader GetXmlReader(string fileName)
		{
			FileInfo fileInfo = new FileInfo(fileName);
			if (!fileInfo.Exists)
			{
				throw new Exception("File not found: " + fileName);
			}
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			return XmlReader.Create(fileInfo.FullName, xmlReaderSettings);
		}

		// Token: 0x04004267 RID: 16999
		private Options _options = new Options();

		// Token: 0x04004268 RID: 17000
		private List<Package> _packages = new List<Package>();

		// Token: 0x02000A7E RID: 2686
		public class CodePointComparer : IComparer
		{
			// Token: 0x06005388 RID: 21384 RVA: 0x00154FC1 File Offset: 0x001531C1
			int IComparer.Compare(object x, object y)
			{
				return x.ToString().CompareTo(y.ToString());
			}
		}
	}
}
