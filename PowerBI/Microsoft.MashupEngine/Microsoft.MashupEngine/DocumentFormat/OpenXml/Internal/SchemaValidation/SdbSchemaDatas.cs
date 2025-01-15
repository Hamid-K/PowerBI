using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using DocumentFormat.OpenXml.Validation;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003122 RID: 12578
	internal class SdbSchemaDatas
	{
		// Token: 0x17009933 RID: 39219
		// (get) Token: 0x0601B488 RID: 111752 RVA: 0x003751D9 File Offset: 0x003733D9
		// (set) Token: 0x0601B489 RID: 111753 RVA: 0x003751E1 File Offset: 0x003733E1
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SdbDataArray<SdbClassIdToSchemaTypeIndex> SdbClassIdMap { get; set; }

		// Token: 0x17009934 RID: 39220
		// (get) Token: 0x0601B48A RID: 111754 RVA: 0x003751EA File Offset: 0x003733EA
		// (set) Token: 0x0601B48B RID: 111755 RVA: 0x003751F2 File Offset: 0x003733F2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SdbDataArray<SdbSchemaType> SdbSchemaTypes { get; set; }

		// Token: 0x17009935 RID: 39221
		// (get) Token: 0x0601B48C RID: 111756 RVA: 0x003751FB File Offset: 0x003733FB
		// (set) Token: 0x0601B48D RID: 111757 RVA: 0x00375203 File Offset: 0x00373403
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SdbDataArray<SdbParticleConstraint> SdbParticles { get; set; }

		// Token: 0x17009936 RID: 39222
		// (get) Token: 0x0601B48E RID: 111758 RVA: 0x0037520C File Offset: 0x0037340C
		// (set) Token: 0x0601B48F RID: 111759 RVA: 0x00375214 File Offset: 0x00373414
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SdbDataArray<SdbParticleChildrenIndex> SdbParticleIndexs { get; set; }

		// Token: 0x17009937 RID: 39223
		// (get) Token: 0x0601B490 RID: 111760 RVA: 0x0037521D File Offset: 0x0037341D
		// (set) Token: 0x0601B491 RID: 111761 RVA: 0x00375225 File Offset: 0x00373425
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SdbDataArray<SdbAttributeConstraint> SdbAttributes { get; set; }

		// Token: 0x17009938 RID: 39224
		// (get) Token: 0x0601B492 RID: 111762 RVA: 0x0037522E File Offset: 0x0037342E
		// (set) Token: 0x0601B493 RID: 111763 RVA: 0x00375236 File Offset: 0x00373436
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private SimpleTypeRestrictions SimpleTypeRestrictions { get; set; }

		// Token: 0x17009939 RID: 39225
		// (get) Token: 0x0601B494 RID: 111764 RVA: 0x0037523F File Offset: 0x0037343F
		// (set) Token: 0x0601B495 RID: 111765 RVA: 0x00375247 File Offset: 0x00373447
		public SdbDataHead SdbDataHead { get; private set; }

		// Token: 0x0601B496 RID: 111766 RVA: 0x00375250 File Offset: 0x00373450
		private SdbSchemaDatas(FileFormatVersions fileFormat)
		{
			this.SdbDataHead = new SdbDataHead();
			this._fileFormat = fileFormat;
		}

		// Token: 0x0601B497 RID: 111767 RVA: 0x00375278 File Offset: 0x00373478
		public static SdbSchemaDatas GetOffice2007SchemaDatas()
		{
			SdbSchemaDatas sdbSchemaDatas = new SdbSchemaDatas(FileFormatVersions.Office2007);
			sdbSchemaDatas.InitOnDemand();
			return sdbSchemaDatas;
		}

		// Token: 0x0601B498 RID: 111768 RVA: 0x00375294 File Offset: 0x00373494
		public static SdbSchemaDatas GetOffice2010SchemaDatas()
		{
			SdbSchemaDatas sdbSchemaDatas = new SdbSchemaDatas(FileFormatVersions.Office2010);
			sdbSchemaDatas.InitOnDemand();
			return sdbSchemaDatas;
		}

		// Token: 0x0601B499 RID: 111769 RVA: 0x003752AF File Offset: 0x003734AF
		internal SchemaTypeData GetSchemaTypeData(OpenXmlElement openxmlElement)
		{
			return this.GetSchemaTypeData(openxmlElement.ElementTypeId);
		}

		// Token: 0x0601B49A RID: 111770 RVA: 0x003752C0 File Offset: 0x003734C0
		internal SchemaTypeData GetSchemaTypeData(int openxmlTypeId)
		{
			this.InitOnDemand();
			ushort num = (ushort)openxmlTypeId;
			SchemaTypeData schemaTypeData;
			if (this._schemaTypeDatas.TryGetValue(num, out schemaTypeData))
			{
				return schemaTypeData;
			}
			schemaTypeData = this.LoadSchemaTypeData(num);
			this._schemaTypeDatas.Add(num, schemaTypeData);
			return schemaTypeData;
		}

		// Token: 0x0601B49B RID: 111771 RVA: 0x003752FE File Offset: 0x003734FE
		private void InitOnDemand()
		{
			if (this._loaded)
			{
				return;
			}
			this.Init();
		}

		// Token: 0x0601B49C RID: 111772 RVA: 0x00375310 File Offset: 0x00373510
		private void Init()
		{
			this._schemaTypeDatas = new Dictionary<ushort, SchemaTypeData>();
			byte[] array;
			switch (this._fileFormat)
			{
			case FileFormatVersions.Office2007:
				array = ValidationResources.O12SchemaConstraintDatas;
				break;
			case FileFormatVersions.Office2010:
				array = ValidationResources.O14SchemaConstraintDatas;
				break;
			default:
				array = ValidationResources.O12SchemaConstraintDatas;
				break;
			}
			using (MemoryStream memoryStream = new MemoryStream(array, false))
			{
				this.Load(memoryStream);
			}
		}

		// Token: 0x0601B49D RID: 111773 RVA: 0x00375384 File Offset: 0x00373584
		private SchemaTypeData LoadSchemaTypeData(ushort openxmlTypeId)
		{
			ushort num = (ushort)((int)openxmlTypeId - this.SdbDataHead.StartClassId);
			SdbSchemaType sdbSchemaType = this.SdbSchemaTypes[(int)this.SdbClassIdMap[(int)num].SchemaTypeIndex];
			AttributeConstraint[] array = this.BuildAttributeConstraint(sdbSchemaType);
			ParticleConstraint particleConstraint = this.BuildParticleConstraint(sdbSchemaType);
			if (particleConstraint != null)
			{
				return new SchemaTypeData((int)openxmlTypeId, array, particleConstraint);
			}
			if (sdbSchemaType.IsSimpleContent)
			{
				SimpleTypeRestriction simpleTypeRestriction = this.SimpleTypeRestrictions[(int)sdbSchemaType.SimpleTypeIndex];
				return new SchemaTypeData((int)openxmlTypeId, array, simpleTypeRestriction);
			}
			return new SchemaTypeData((int)openxmlTypeId, array);
		}

		// Token: 0x0601B49E RID: 111774 RVA: 0x00375408 File Offset: 0x00373608
		private ParticleConstraint BuildParticleConstraint(SdbSchemaType sdbSchemaTpye)
		{
			if (sdbSchemaTpye.IsCompositeType)
			{
				return this.BuildParticleConstraint(sdbSchemaTpye.ParticleIndex);
			}
			return null;
		}

		// Token: 0x0601B49F RID: 111775 RVA: 0x00375430 File Offset: 0x00373630
		private ParticleConstraint BuildParticleConstraint(ushort particleIndex)
		{
			SdbParticleConstraint sdbParticleConstraint = this.SdbParticles[(int)particleIndex];
			ParticleConstraint particleConstraint = ParticleConstraint.CreateParticleConstraint(sdbParticleConstraint.ParticleType);
			particleConstraint.ParticleType = sdbParticleConstraint.ParticleType;
			particleConstraint.MaxOccurs = sdbParticleConstraint.MaxOccurs;
			particleConstraint.MinOccurs = (int)sdbParticleConstraint.MinOccurs;
			particleConstraint.ElementId = (int)sdbParticleConstraint.ElementTypeId;
			if (sdbParticleConstraint.ChildrenCount > 0)
			{
				particleConstraint.ChildrenParticles = new ParticleConstraint[(int)sdbParticleConstraint.ChildrenCount];
				for (ushort num = 0; num < sdbParticleConstraint.ChildrenCount; num += 1)
				{
					ushort particleIndex2 = this.SdbParticleIndexs[(int)(sdbParticleConstraint.ChildrenStartIndex + num)].ParticleIndex;
					particleConstraint.ChildrenParticles[(int)num] = this.BuildParticleConstraint(particleIndex2);
				}
			}
			else if (sdbParticleConstraint.ParticleType == ParticleType.All || sdbParticleConstraint.ParticleType == ParticleType.Choice || sdbParticleConstraint.ParticleType == ParticleType.Group || sdbParticleConstraint.ParticleType == ParticleType.Sequence)
			{
				particleConstraint.ChildrenParticles = this.EmptyChildrenParticles;
			}
			return particleConstraint;
		}

		// Token: 0x0601B4A0 RID: 111776 RVA: 0x00375510 File Offset: 0x00373710
		private AttributeConstraint[] BuildAttributeConstraint(SdbSchemaType sdbSchemaTpye)
		{
			if (sdbSchemaTpye.AttributesCount > 0)
			{
				int attributesCount = (int)sdbSchemaTpye.AttributesCount;
				AttributeConstraint[] array = new AttributeConstraint[attributesCount];
				ushort startIndexOfAttributes = sdbSchemaTpye.StartIndexOfAttributes;
				for (int i = 0; i < attributesCount; i++)
				{
					SdbAttributeConstraint sdbAttributeConstraint = this.SdbAttributes[(int)startIndexOfAttributes + i];
					ushort simpleTypeIndex = sdbAttributeConstraint.SimpleTypeIndex;
					SimpleTypeRestriction simpleTypeRestriction = this.SimpleTypeRestrictions[(int)simpleTypeIndex];
					array[i] = new AttributeConstraint(sdbAttributeConstraint.AttributeUse, simpleTypeRestriction, (FileFormatVersions)sdbAttributeConstraint.FileFormatVersion);
				}
				return array;
			}
			return null;
		}

		// Token: 0x0601B4A1 RID: 111777 RVA: 0x0037558C File Offset: 0x0037378C
		internal void Load(Stream dataStream)
		{
			byte[] array = new byte[128];
			dataStream.Read(array, 0, 128);
			this.SdbDataHead.LoadFromBytes(array, 0);
			this.CheckDataHead((int)dataStream.Length);
			int num = this.SdbDataHead.ClassIdsCount * SdbClassIdToSchemaTypeIndex.TypeSize;
			byte[] array2 = new byte[num];
			dataStream.Read(array2, 0, num);
			this.SdbClassIdMap = new SdbDataArray<SdbClassIdToSchemaTypeIndex>(array2);
			num = this.SdbDataHead.SchemaTypeCount * SdbSchemaType.TypeSize;
			array2 = new byte[num];
			dataStream.Read(array2, 0, num);
			this.SdbSchemaTypes = new SdbDataArray<SdbSchemaType>(array2);
			num = this.SdbDataHead.ParticleCount * SdbParticleConstraint.TypeSize;
			array2 = new byte[num];
			dataStream.Read(array2, 0, num);
			this.SdbParticles = new SdbDataArray<SdbParticleConstraint>(array2);
			num = this.SdbDataHead.ParticleChildrenIndexCount * SdbParticleChildrenIndex.TypeSize;
			array2 = new byte[num];
			dataStream.Read(array2, 0, num);
			this.SdbParticleIndexs = new SdbDataArray<SdbParticleChildrenIndex>(array2);
			num = this.SdbDataHead.AttributeCount * SdbAttributeConstraint.TypeSize;
			array2 = new byte[num];
			dataStream.Read(array2, 0, num);
			this.SdbAttributes = new SdbDataArray<SdbAttributeConstraint>(array2);
			dataStream.Seek((long)this.SdbDataHead.SimpleTypeDataOffset, SeekOrigin.Begin);
			this.SimpleTypeRestrictions = SimpleTypeRestrictions.Deserialize(dataStream, this._fileFormat);
			SdbSchemaDatas.Assert(this.SdbDataHead.SimpleTypeCount == this.SimpleTypeRestrictions.SimpleTypeCount);
			this.CheckData();
			this._loaded = true;
		}

		// Token: 0x0601B4A2 RID: 111778 RVA: 0x00375708 File Offset: 0x00373908
		private SdbClassIdToSchemaTypeIndex GetClassIdData(ushort classId)
		{
			int num = (int)SdbClassIdToSchemaTypeIndex.ArrayIndexFromClassId(classId);
			return this.SdbClassIdMap[num];
		}

		// Token: 0x0601B4A3 RID: 111779 RVA: 0x00375728 File Offset: 0x00373928
		private void CheckDataHead(int streamLength)
		{
			SdbDataHead sdbDataHead = this.SdbDataHead;
			SdbSchemaDatas.Assert(sdbDataHead.StartClassId == 10001);
			SdbSchemaDatas.Assert(sdbDataHead.DataByteCount + 256 == streamLength);
			SdbSchemaDatas.Assert(sdbDataHead.ClassIdsCount > 0);
			SdbSchemaDatas.Assert(sdbDataHead.SchemaTypeCount > 0);
			SdbSchemaDatas.Assert(sdbDataHead.SchemaTypeDataOffset == sdbDataHead.ClassIdsDataOffset + sdbDataHead.ClassIdsCount * SdbClassIdToSchemaTypeIndex.TypeSize);
			SdbSchemaDatas.Assert(sdbDataHead.ParticleCount > 0);
			SdbSchemaDatas.Assert(sdbDataHead.ParticleDataOffset == sdbDataHead.SchemaTypeDataOffset + sdbDataHead.SchemaTypeCount * SdbSchemaType.TypeSize);
			SdbSchemaDatas.Assert(sdbDataHead.ParticleChildrenIndexCount > 0);
			SdbSchemaDatas.Assert(sdbDataHead.ParticleChildrenIndexDataOffset == sdbDataHead.ParticleDataOffset + sdbDataHead.ParticleCount * SdbParticleConstraint.TypeSize);
			SdbSchemaDatas.Assert(sdbDataHead.AttributeCount > 0);
			SdbSchemaDatas.Assert(sdbDataHead.AttributeDataOffset == sdbDataHead.ParticleChildrenIndexDataOffset + sdbDataHead.ParticleChildrenIndexCount * SdbParticleChildrenIndex.TypeSize);
			SdbSchemaDatas.Assert(sdbDataHead.SimpleTypeCount > 0);
			SdbSchemaDatas.Assert(sdbDataHead.SimpleTypeDataOffset == sdbDataHead.AttributeDataOffset + sdbDataHead.AttributeCount * SdbAttributeConstraint.TypeSize);
			SdbSchemaDatas.Assert(sdbDataHead.SimpleTypeDataOffset < streamLength);
		}

		// Token: 0x0601B4A4 RID: 111780 RVA: 0x00375864 File Offset: 0x00373A64
		private static void Assert(bool value)
		{
			if (!value)
			{
				throw new InvalidDataException("Invalid schema constraint binary data.");
			}
		}

		// Token: 0x0601B4A5 RID: 111781 RVA: 0x0000336E File Offset: 0x0000156E
		private void CheckData()
		{
		}

		// Token: 0x0400B4D0 RID: 46288
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ParticleConstraint[] EmptyChildrenParticles = new ParticleConstraint[0];

		// Token: 0x0400B4D1 RID: 46289
		private Dictionary<ushort, SchemaTypeData> _schemaTypeDatas;

		// Token: 0x0400B4D2 RID: 46290
		private bool _loaded;

		// Token: 0x0400B4D3 RID: 46291
		private FileFormatVersions _fileFormat;
	}
}
