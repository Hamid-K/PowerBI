using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000162 RID: 354
	[DataContract(Name = "ConceptualProperty", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualProperty
	{
		// Token: 0x060008E7 RID: 2279 RVA: 0x00012440 File Offset: 0x00010640
		internal ClientConceptualProperty()
		{
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00012448 File Offset: 0x00010648
		internal ClientConceptualProperty(string name, string edmName, string displayName, string description, ConceptualPrimitiveType dataType, ConceptualDataCategory dataCategory, bool hidden, bool isPrivate, string formatString, ClientConceptualColumn column, ClientConceptualMeasure measure, ClientConceptualQueryableState queryable, string errorMessage, bool canDelete, string stableName)
		{
			this.Name = name;
			if (name != edmName)
			{
				this.EdmName = edmName;
			}
			if (name != displayName)
			{
				this.DisplayName = displayName;
			}
			this.Description = description;
			this.DataType = (int)dataType;
			this.FormatString = formatString;
			this.Hidden = hidden;
			this.Private = isPrivate;
			this.Column = column;
			this.Measure = measure;
			this.Queryable = queryable;
			this.ErrorMessage = errorMessage;
			this.CanDelete = ClientConceptualSchemaFactory.ConvertTrueToNull(canDelete);
			if (dataCategory != ConceptualDataCategory.None)
			{
				this.DataCategory = dataCategory.ToString();
			}
			this.StableName = stableName;
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x000124F6 File Offset: 0x000106F6
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x000124FE File Offset: 0x000106FE
		[DataMember(Name = "Name", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		internal string Name { get; private set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00012507 File Offset: 0x00010707
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0001250F File Offset: 0x0001070F
		[DataMember(Name = "EdmName", IsRequired = false, EmitDefaultValue = false, Order = 1)]
		internal string EdmName { get; private set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00012518 File Offset: 0x00010718
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x00012520 File Offset: 0x00010720
		[DataMember(Name = "DisplayName", IsRequired = false, EmitDefaultValue = false, Order = 2)]
		internal string DisplayName { get; private set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00012529 File Offset: 0x00010729
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x00012531 File Offset: 0x00010731
		[DataMember(Name = "DataType", IsRequired = true, EmitDefaultValue = true, Order = 3)]
		internal int DataType { get; private set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x0001253A File Offset: 0x0001073A
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x00012542 File Offset: 0x00010742
		[DataMember(Name = "DataCategory", IsRequired = false, EmitDefaultValue = false, Order = 4)]
		internal string DataCategory { get; private set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060008F3 RID: 2291 RVA: 0x0001254B File Offset: 0x0001074B
		// (set) Token: 0x060008F4 RID: 2292 RVA: 0x00012553 File Offset: 0x00010753
		[DataMember(Name = "Hidden", IsRequired = false, EmitDefaultValue = false, Order = 5)]
		internal bool Hidden { get; private set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060008F5 RID: 2293 RVA: 0x0001255C File Offset: 0x0001075C
		// (set) Token: 0x060008F6 RID: 2294 RVA: 0x00012564 File Offset: 0x00010764
		[DataMember(Name = "FormatString", IsRequired = false, EmitDefaultValue = false, Order = 6)]
		internal string FormatString { get; private set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x0001256D File Offset: 0x0001076D
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00012575 File Offset: 0x00010775
		[DataMember(Name = "Column", IsRequired = false, EmitDefaultValue = false, Order = 7)]
		internal ClientConceptualColumn Column { get; private set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x0001257E File Offset: 0x0001077E
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00012586 File Offset: 0x00010786
		[DataMember(Name = "Measure", IsRequired = false, EmitDefaultValue = false, Order = 8)]
		internal ClientConceptualMeasure Measure { get; private set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x0001258F File Offset: 0x0001078F
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00012597 File Offset: 0x00010797
		[DataMember(Name = "Queryable", IsRequired = false, EmitDefaultValue = false, Order = 9)]
		internal ClientConceptualQueryableState Queryable { get; private set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x000125A0 File Offset: 0x000107A0
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x000125A8 File Offset: 0x000107A8
		[DataMember(Name = "ErrorMessage", IsRequired = false, EmitDefaultValue = false, Order = 10)]
		internal string ErrorMessage { get; private set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x000125B1 File Offset: 0x000107B1
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x000125B9 File Offset: 0x000107B9
		[DataMember(Name = "CanDelete", IsRequired = false, EmitDefaultValue = false, Order = 11)]
		internal bool? CanDelete { get; private set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x000125C2 File Offset: 0x000107C2
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x000125CA File Offset: 0x000107CA
		[DataMember(Name = "Description", IsRequired = false, EmitDefaultValue = false, Order = 12)]
		internal string Description { get; private set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x000125D3 File Offset: 0x000107D3
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x000125DB File Offset: 0x000107DB
		[DataMember(Name = "Private", IsRequired = false, EmitDefaultValue = false, Order = 14)]
		internal bool Private { get; private set; }

		// Token: 0x04000487 RID: 1159
		[DataMember(Name = "StableName", IsRequired = false, EmitDefaultValue = false, Order = 13)]
		internal string StableName;
	}
}
