using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000140 RID: 320
	[DebuggerDisplay("TmdlObject - type={ObjectType}: name=\"{Name}\"")]
	internal sealed class TmdlObject
	{
		// Token: 0x060014ED RID: 5357 RVA: 0x0008CD8E File Offset: 0x0008AF8E
		public TmdlObject(ObjectType objectType)
		{
			this.ObjectType = objectType;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x0008CD9D File Offset: 0x0008AF9D
		public TmdlObject(string propertyName)
		{
			this.ObjectType = ObjectType.Null;
			this.Name = new ObjectName(new string[] { propertyName });
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0008CDC1 File Offset: 0x0008AFC1
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x0008CDC9 File Offset: 0x0008AFC9
		public TmdlSourceLocation SourceLocation { get; internal set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0008CDD2 File Offset: 0x0008AFD2
		public ObjectType ObjectType { get; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x0008CDDA File Offset: 0x0008AFDA
		// (set) Token: 0x060014F3 RID: 5363 RVA: 0x0008CDE2 File Offset: 0x0008AFE2
		public ObjectName Name { get; set; }

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x0008CDEB File Offset: 0x0008AFEB
		// (set) Token: 0x060014F5 RID: 5365 RVA: 0x0008CDF3 File Offset: 0x0008AFF3
		public int? Ordinal { get; set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0008CDFC File Offset: 0x0008AFFC
		// (set) Token: 0x060014F7 RID: 5367 RVA: 0x0008CE04 File Offset: 0x0008B004
		public string[] Description { get; set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0008CE0D File Offset: 0x0008B00D
		// (set) Token: 0x060014F9 RID: 5369 RVA: 0x0008CE15 File Offset: 0x0008B015
		public TmdlProperty DefaultProperty { get; set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x0008CE1E File Offset: 0x0008B01E
		// (set) Token: 0x060014FB RID: 5371 RVA: 0x0008CE26 File Offset: 0x0008B026
		public bool IsReference
		{
			get
			{
				return this.isReference;
			}
			set
			{
				this.isReference = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x0008CE2F File Offset: 0x0008B02F
		public ICollection<TmdlProperty> Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new List<TmdlProperty>();
				}
				return this.properties;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0008CE4A File Offset: 0x0008B04A
		public ICollection<TmdlObject> Children
		{
			get
			{
				if (this.children == null)
				{
					this.children = new List<TmdlObject>();
				}
				return this.children;
			}
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x0008CE65 File Offset: 0x0008B065
		public void AddDeprecatedProperty(TmdlProperty property)
		{
			if (this.deprecatedProperties == null)
			{
				this.deprecatedProperties = new List<TmdlProperty>();
			}
			this.deprecatedProperties.Add(property);
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x0008CE88 File Offset: 0x0008B088
		public bool TryGetDeprecatedProperty(string name, out TmdlProperty property)
		{
			if (this.deprecatedProperties != null)
			{
				property = this.deprecatedProperties.FirstOrDefault((TmdlProperty p) => string.Compare(name, p.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
			}
			else
			{
				property = null;
			}
			return property != null;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x0008CED0 File Offset: 0x0008B0D0
		public void CopyTo(TmdlObject other)
		{
			if (this.IsReference)
			{
				throw new InvalidOperationException(TomSR.Exception_TmdlRefObjectCopy);
			}
			other.Name = this.Name;
			other.Description = this.Description;
			other.DefaultProperty = this.DefaultProperty;
			if (this.properties != null)
			{
				foreach (TmdlProperty tmdlProperty in this.properties)
				{
					other.Properties.Add(tmdlProperty);
				}
			}
			if (this.children != null)
			{
				foreach (TmdlObject tmdlObject in this.children)
				{
					other.Children.Add(tmdlObject);
				}
			}
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x0008CFB8 File Offset: 0x0008B1B8
		public void WriteTo(ITmdlWriter writer)
		{
			if (!this.IsReference && this.Description != null && !this.Description.IsEmpty())
			{
				foreach (string text in this.Description)
				{
					writer.WriteLine("{0} {1}", new object[] { "///", text });
				}
			}
			this.WriteObjectHeader(writer);
			if ((!this.IsReference && this.properties != null && this.properties.Count > 0) || (this.children != null && this.children.Count > 0))
			{
				IDisposable disposable = (((this.ObjectType != ObjectType.Model && this.ObjectType != ObjectType.Database) || (this.properties != null && this.properties.Count > 0)) ? writer.Indent(1) : null);
				try
				{
					if (!this.IsReference)
					{
						this.WriteProperties(writer);
						if (this.children != null && this.children.Count > 0)
						{
							foreach (TmdlObject tmdlObject in this.children.Where((TmdlObject c) => c.ObjectType == ObjectType.Null && (c.HasAnyProperty(true) || c.HasAnyChild(true))))
							{
								tmdlObject.WriteTo(writer);
							}
						}
					}
					if ((this.ObjectType == ObjectType.Model || this.ObjectType == ObjectType.Database) && disposable != null)
					{
						disposable.Dispose();
						disposable = null;
					}
					if (this.ObjectType != ObjectType.Null && this.ObjectType != ObjectType.RoleMembership)
					{
						writer.WriteLine();
					}
					if (this.children != null && this.children.Count > 0)
					{
						List<TmdlObject> list = new List<TmdlObject>();
						bool flag = this.ObjectType == ObjectType.Role;
						bool flag2 = false;
						foreach (TmdlObject tmdlObject2 in this.children.Where((TmdlObject c) => c.ObjectType > ObjectType.Null))
						{
							if (tmdlObject2.IsEmptyReferenceStub())
							{
								list.Add(tmdlObject2);
							}
							else
							{
								if (flag)
								{
									if (tmdlObject2.ObjectType == ObjectType.RoleMembership)
									{
										flag2 = true;
									}
									else if (flag2)
									{
										writer.WriteLine();
										flag2 = false;
									}
								}
								tmdlObject2.WriteTo(writer);
							}
						}
						if (list.Count > 0)
						{
							TmdlObject.WriteIndexingRefObjects(writer, list);
						}
					}
					return;
				}
				finally
				{
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (this.ObjectType != ObjectType.Null && this.ObjectType != ObjectType.RoleMembership)
			{
				writer.WriteLine();
			}
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x0008D28C File Offset: 0x0008B48C
		internal static void WriteIndexingRefObjects(ITmdlWriter writer, IList<TmdlObject> indexingRefObjects)
		{
			for (int i = 0; i < indexingRefObjects.Count; i++)
			{
				ObjectType objectType = indexingRefObjects[i].ObjectType;
				indexingRefObjects[i].WriteObjectHeader(writer);
				int j = i + 1;
				while (j < indexingRefObjects.Count)
				{
					if (indexingRefObjects[j].ObjectType == objectType)
					{
						indexingRefObjects[j].WriteObjectHeader(writer);
						indexingRefObjects.RemoveAt(j);
					}
					else
					{
						j++;
					}
				}
				writer.WriteLine();
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0008D302 File Offset: 0x0008B502
		internal bool IsEmptyReferenceStub()
		{
			return this.IsReference && (this.children == null || this.children.Count == 0);
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x0008D326 File Offset: 0x0008B526
		internal bool HasAnyProperty(bool includeDefaultProperty)
		{
			return (includeDefaultProperty && this.DefaultProperty != null) || (this.properties != null && this.properties.Count > 0);
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0008D350 File Offset: 0x0008B550
		internal bool HasAnyChild(bool includeNoneMetadataObjects)
		{
			if (this.children == null || this.children.Count == 0)
			{
				return false;
			}
			if (includeNoneMetadataObjects)
			{
				return true;
			}
			return this.children.Any((TmdlObject c) => c.ObjectType > ObjectType.Null);
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x0008D3A4 File Offset: 0x0008B5A4
		internal void AddContentOf(TmdlObject other)
		{
			Utils.Verify(other != null);
			Utils.Verify(this.ObjectType == other.ObjectType);
			Utils.Verify((this.Name.IsEmpty && other.Name.IsEmpty) || (!this.Name.IsEmpty && !other.Name.IsEmpty && this.Name == other.Name));
			if (this.Description != null && !this.Description.IsEmpty() && other.Description != null && !other.Description.IsEmpty())
			{
				throw TmdlSerializationException.CreateAmbiguousSourceException(TomSR.TmdlAmbiguousSourceError_DuplicateDescription, this, other);
			}
			if (this.Ordinal != null && other.Ordinal != null && other.Ordinal.Value != this.Ordinal.Value)
			{
				throw TmdlSerializationException.CreateAmbiguousSourceException(TomSR.TmdlAmbiguousSourceError_DuplicateOrdinal, this, other);
			}
			if (this.IsReference && !other.IsReference)
			{
				this.IsReference = false;
				this.SourceLocation = other.SourceLocation;
			}
			if (this.Name.IsEmpty)
			{
				this.Name = other.Name;
			}
			if (this.Description == null || this.Description.IsEmpty())
			{
				this.Description = other.Description;
			}
			if (this.Ordinal == null && other.Ordinal != null)
			{
				this.Ordinal = new int?(other.Ordinal.Value);
			}
			if (other.properties != null && other.properties.Count > 0)
			{
				if (this.properties == null)
				{
					this.properties = new List<TmdlProperty>();
				}
				using (List<TmdlProperty>.Enumerator enumerator = other.properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TmdlProperty property2 = enumerator.Current;
						if (this.properties.Any((TmdlProperty p) => string.Compare(p.Name, property2.Name, StringComparison.InvariantCultureIgnoreCase) == 0))
						{
							throw TmdlSerializationException.CreateAmbiguousSourceException(TomSR.TmdlAmbiguousSourceError_DuplicateProperty(property2.Name), this, other);
						}
						this.properties.Add(property2);
					}
				}
			}
			if (other.deprecatedProperties != null && other.deprecatedProperties.Count > 0)
			{
				if (this.deprecatedProperties == null)
				{
					this.deprecatedProperties = new List<TmdlProperty>();
				}
				using (List<TmdlProperty>.Enumerator enumerator = other.deprecatedProperties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						TmdlProperty property = enumerator.Current;
						if (this.deprecatedProperties.Any((TmdlProperty p) => string.Compare(p.Name, property.Name, StringComparison.InvariantCultureIgnoreCase) == 0))
						{
							throw TmdlSerializationException.CreateAmbiguousSourceException(TomSR.TmdlAmbiguousSourceError_DuplicateProperty(property.Name), this, other);
						}
						this.deprecatedProperties.Add(property);
					}
				}
			}
			if (other.children != null && other.children.Count > 0)
			{
				if (this.children == null)
				{
					this.children = new List<TmdlObject>();
				}
				this.children.AddRange(other.children);
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x0008D6F8 File Offset: 0x0008B8F8
		internal void WriteProperties(ITmdlWriter writer)
		{
			if (this.properties != null)
			{
				foreach (TmdlProperty tmdlProperty in this.properties.Where((TmdlProperty p) => !p.DoNotSerialize))
				{
					tmdlProperty.WriteTo(writer);
				}
			}
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x0008D770 File Offset: 0x0008B970
		private void WriteObjectHeader(ITmdlWriter writer)
		{
			ObjectType objectType = this.ObjectType;
			string text;
			string text2;
			if (objectType <= ObjectType.Culture)
			{
				if (objectType != ObjectType.Null)
				{
					if (objectType != ObjectType.Culture)
					{
						goto IL_0137;
					}
					text = writer.FormatKeyword("CultureInfo");
					text2 = (this.Name.IsEmpty ? string.Empty : string.Format(CultureInfo.InvariantCulture, " {0}", this.Name.FullyQualifiedName));
					goto IL_01F2;
				}
			}
			else
			{
				if (objectType == ObjectType.RoleMembership)
				{
					text = writer.FormatKeyword("Member");
					text2 = (this.Name.IsEmpty ? string.Empty : string.Format(CultureInfo.InvariantCulture, " {0}", this.Name.FullyQualifiedName));
					goto IL_01F2;
				}
				if (objectType != ObjectType.CalculationExpression)
				{
					if (objectType != ObjectType.Database)
					{
						goto IL_0137;
					}
					text = writer.FormatKeyword(ObjectType.Database.ToString("G"));
					text2 = (this.Name.IsEmpty ? string.Empty : string.Format(CultureInfo.InvariantCulture, " {0}", this.Name.FullyQualifiedName));
					goto IL_01F2;
				}
			}
			text = writer.FormatKeyword(this.Name.Name);
			text2 = string.Empty;
			goto IL_01F2;
			IL_0137:
			if (ObjectTreeHelper.IsNamedObject(this.ObjectType) || ObjectTreeHelper.IsKeyedObject(this.ObjectType))
			{
				text = writer.FormatKeyword(this.ObjectType.ToString("G"));
				text2 = (this.Name.IsEmpty ? string.Empty : string.Format(CultureInfo.InvariantCulture, " {0}", this.Name.FullyQualifiedName));
			}
			else
			{
				if (!string.IsNullOrEmpty(this.Name.Name))
				{
					text = writer.FormatKeyword(this.Name.Name);
				}
				else
				{
					text = writer.FormatKeyword(this.ObjectType.ToString("G"));
				}
				text2 = string.Empty;
			}
			IL_01F2:
			if (!this.IsReference && this.DefaultProperty != null)
			{
				TmdlStringValue tmdlStringValue = this.DefaultProperty.Value as TmdlStringValue;
				if (tmdlStringValue != null && tmdlStringValue.ContainsExpressionThatRequiresQuotes)
				{
					writer.Write("{0}{1} = {2}", new object[] { text, text2, "```" });
					this.DefaultProperty.Value.WriteTo(writer, new int?(2));
					writer.WriteLineWithAdditionalIndentation(2, "```", Array.Empty<object>());
					return;
				}
				if (this.DefaultProperty.Value.HasBody)
				{
					writer.Write("{0}{1} =", new object[] { text, text2 });
					this.DefaultProperty.Value.WriteTo(writer, new int?(2));
					return;
				}
				writer.Write("{0}{1} = ", new object[] { text, text2 });
				this.DefaultProperty.Value.WriteTo(writer, null);
				return;
			}
			else
			{
				if (this.IsReference)
				{
					writer.WriteLine("{0} {1}{2}", new object[] { "ref", text, text2 });
					return;
				}
				writer.WriteLine("{0}{1}", new object[] { text, text2 });
				return;
			}
		}

		// Token: 0x04000388 RID: 904
		private bool isReference;

		// Token: 0x04000389 RID: 905
		private List<TmdlProperty> properties;

		// Token: 0x0400038A RID: 906
		private List<TmdlProperty> deprecatedProperties;

		// Token: 0x0400038B RID: 907
		private List<TmdlObject> children;

		// Token: 0x0200031E RID: 798
		internal static class TmdlObjectType
		{
			// Token: 0x04000DB9 RID: 3513
			public const string Culture = "CultureInfo";

			// Token: 0x04000DBA RID: 3514
			public const string RoleMember = "Member";
		}
	}
}
