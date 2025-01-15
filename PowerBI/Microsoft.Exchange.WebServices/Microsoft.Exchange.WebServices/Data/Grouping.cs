using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002FB RID: 763
	public sealed class Grouping : ISelfValidate, IJsonSerializable
	{
		// Token: 0x06001B08 RID: 6920 RVA: 0x000487FE File Offset: 0x000477FE
		private void InternalValidate()
		{
			EwsUtilities.ValidateParam(this.GroupOn, "GroupOn");
			EwsUtilities.ValidateParam(this.AggregateOn, "AggregateOn");
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00048820 File Offset: 0x00047820
		public Grouping()
		{
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x00048828 File Offset: 0x00047828
		public Grouping(PropertyDefinitionBase groupOn, SortDirection sortDirection, PropertyDefinitionBase aggregateOn, AggregateType aggregateType)
			: this()
		{
			EwsUtilities.ValidateParam(groupOn, "groupOn");
			EwsUtilities.ValidateParam(aggregateOn, "aggregateOn");
			this.groupOn = groupOn;
			this.sortDirection = sortDirection;
			this.aggregateOn = aggregateOn;
			this.aggregateType = aggregateType;
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x00048864 File Offset: 0x00047864
		internal void WriteToXml(EwsServiceXmlWriter writer)
		{
			writer.WriteStartElement(XmlNamespace.Messages, "GroupBy");
			writer.WriteAttributeValue("Order", this.SortDirection);
			this.GroupOn.WriteToXml(writer);
			writer.WriteStartElement(XmlNamespace.Types, "AggregateOn");
			writer.WriteAttributeValue("Aggregate", this.AggregateType);
			this.AggregateOn.WriteToXml(writer);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x000488DC File Offset: 0x000478DC
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("Order", this.SortDirection);
			throw new NotImplementedException();
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x0004890A File Offset: 0x0004790A
		// (set) Token: 0x06001B0E RID: 6926 RVA: 0x00048912 File Offset: 0x00047912
		public SortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			set
			{
				this.sortDirection = value;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x0004891B File Offset: 0x0004791B
		// (set) Token: 0x06001B10 RID: 6928 RVA: 0x00048923 File Offset: 0x00047923
		public PropertyDefinitionBase GroupOn
		{
			get
			{
				return this.groupOn;
			}
			set
			{
				this.groupOn = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x0004892C File Offset: 0x0004792C
		// (set) Token: 0x06001B12 RID: 6930 RVA: 0x00048934 File Offset: 0x00047934
		public PropertyDefinitionBase AggregateOn
		{
			get
			{
				return this.aggregateOn;
			}
			set
			{
				this.aggregateOn = value;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x0004893D File Offset: 0x0004793D
		// (set) Token: 0x06001B14 RID: 6932 RVA: 0x00048945 File Offset: 0x00047945
		public AggregateType AggregateType
		{
			get
			{
				return this.aggregateType;
			}
			set
			{
				this.aggregateType = value;
			}
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x0004894E File Offset: 0x0004794E
		void ISelfValidate.Validate()
		{
			this.InternalValidate();
		}

		// Token: 0x0400143A RID: 5178
		private SortDirection sortDirection;

		// Token: 0x0400143B RID: 5179
		private PropertyDefinitionBase groupOn;

		// Token: 0x0400143C RID: 5180
		private PropertyDefinitionBase aggregateOn;

		// Token: 0x0400143D RID: 5181
		private AggregateType aggregateType;
	}
}
