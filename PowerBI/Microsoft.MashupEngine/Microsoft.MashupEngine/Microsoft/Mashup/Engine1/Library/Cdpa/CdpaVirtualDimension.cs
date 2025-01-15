using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D97 RID: 3479
	internal class CdpaVirtualDimension : CdpaDimension
	{
		// Token: 0x06005EB0 RID: 24240 RVA: 0x001473BC File Offset: 0x001455BC
		public CdpaVirtualDimension(CdpaCube cube, RecordValue dimension)
		{
			this.cube = cube;
			this.name = dimension["name"].AsString;
			this.qualifiedName = QualifiedName.New(this.name);
			this.attributes = new Dictionary<QualifiedName, CdpaDimensionAttribute>();
			this.PopulateDimension(dimension);
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x06005EB1 RID: 24241 RVA: 0x0014740F File Offset: 0x0014560F
		public override CdpaCube Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x06005EB2 RID: 24242 RVA: 0x00147417 File Offset: 0x00145617
		public override QualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x06005EB3 RID: 24243 RVA: 0x0014741F File Offset: 0x0014561F
		public override string Caption
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x06005EB4 RID: 24244 RVA: 0x00147427 File Offset: 0x00145627
		public override IDictionary<QualifiedName, CdpaDimensionAttribute> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x06005EB5 RID: 24245 RVA: 0x00147430 File Offset: 0x00145630
		private void PopulateDimension(RecordValue dimension)
		{
			foreach (IValueReference valueReference in dimension["columns"].AsList)
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				string asString = asRecord["name"].AsString;
				TypeValue mtype = CdpaCube.GetMType(asRecord["type"].AsString);
				Value value;
				if (!asRecord.TryGetValue("expression", out value) || value.IsNull || value.AsString.Length == 0)
				{
					CdpaDimensionAttribute cdpaDimensionAttribute = new CdpaRelatedDimensionAttribute(this, asString, asString, mtype);
					this.attributes.Add(cdpaDimensionAttribute.QualifiedName, cdpaDimensionAttribute);
					Value value2;
					if (asRecord.TryGetValue("isDefaultTimeSeriesAttribute", out value2) && value2.IsLogical && value2.AsBoolean && cdpaDimensionAttribute.Type.TypeKind == ValueKind.DateTime)
					{
						this.cube.AddTimestampAttribute(cdpaDimensionAttribute);
					}
				}
			}
			foreach (IValueReference valueReference2 in dimension["columns"].AsList)
			{
				RecordValue asRecord2 = valueReference2.Value.AsRecord;
				string asString2 = asRecord2["name"].AsString;
				TypeValue mtype2 = CdpaCube.GetMType(asRecord2["type"].AsString);
				Value value3;
				if (asRecord2.TryGetValue("expression", out value3) && !value3.IsNull && value3.AsString.Length != 0)
				{
					RecordTypeValue recordType = CdpaCube.GetRecordType(this.attributes.Values);
					CubeExpression cubeExpression = this.cube.GetCubeExpression(recordType, value3.AsString, this.QualifiedName);
					CdpaDimensionAttribute cdpaDimensionAttribute2 = new CdpaProjectedDimensionAttribute(this, asString2, asString2, mtype2, cubeExpression);
					this.attributes.Add(cdpaDimensionAttribute2.QualifiedName, cdpaDimensionAttribute2);
				}
			}
			Value value4;
			if (dimension.TryGetValue("hierarchies", out value4))
			{
				foreach (IValueReference valueReference3 in value4.AsList)
				{
					RecordValue asRecord3 = valueReference3.Value.AsRecord;
					string asString3 = asRecord3["name"].AsString;
					CdpaHierarchy cdpaHierarchy = new CdpaHierarchy(this, asString3, asString3);
					foreach (IValueReference valueReference4 in asRecord3["levels"].AsList)
					{
						RecordValue asRecord4 = valueReference4.Value.AsRecord;
						string asString4 = asRecord4["name"].AsString;
						int num = (int)asRecord4["ordinal"].AsNumber.AsInteger64;
						string asString5 = asRecord4["column"].AsString;
						CdpaDimensionAttribute cdpaDimensionAttribute3 = this.attributes[this.QualifiedName.Qualify(asString5)];
						CdpaHierarchyLevel cdpaHierarchyLevel = new CdpaHierarchyLevel(cdpaHierarchy, num, asString4, asString4, cdpaDimensionAttribute3);
						cdpaHierarchy.Levels.Add(cdpaHierarchyLevel);
						this.attributes.Add(cdpaHierarchyLevel.QualifiedName, cdpaHierarchyLevel);
						this.attributes.Remove(cdpaHierarchyLevel.Attribute.QualifiedName);
						this.cube.Attributes[cdpaHierarchyLevel.Attribute.QualifiedName] = cdpaHierarchyLevel.Attribute;
						IList<CdpaHierarchyLevel> list;
						if (!this.cube.AttributeLevels.TryGetValue(cdpaDimensionAttribute3, out list))
						{
							list = new List<CdpaHierarchyLevel>();
							this.cube.AttributeLevels.Add(cdpaDimensionAttribute3, list);
						}
						list.Add(cdpaHierarchyLevel);
					}
				}
			}
		}

		// Token: 0x04003401 RID: 13313
		private readonly CdpaCube cube;

		// Token: 0x04003402 RID: 13314
		private readonly QualifiedName qualifiedName;

		// Token: 0x04003403 RID: 13315
		private readonly string name;

		// Token: 0x04003404 RID: 13316
		private readonly Dictionary<QualifiedName, CdpaDimensionAttribute> attributes;
	}
}
