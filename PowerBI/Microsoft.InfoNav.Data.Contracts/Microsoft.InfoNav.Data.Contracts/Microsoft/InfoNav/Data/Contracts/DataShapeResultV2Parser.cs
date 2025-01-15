using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.DataShapeResult;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000085 RID: 133
	internal sealed class DataShapeResultV2Parser : DataShapeResultParserBase
	{
		// Token: 0x0600030E RID: 782 RVA: 0x00008714 File Offset: 0x00006914
		internal DataShapeResultV2Parser(bool fillEmptyIntersections)
			: base(DsrNames.V2)
		{
			this._fillEmptyIntersections = fillEmptyIntersections;
			Util.EnsureDictionary<string, DsrCalculationsBuilder>(ref this._dataMemberIdToBuilderCache, StringComparer.Ordinal);
			Util.EnsureDictionary<string, DsrIntersectionCalculationsBuilder>(ref this._intersectionIdToBuilderCache, StringComparer.Ordinal);
			Util.EnsureDictionary<string, DsrIntersectionCalculationsBuilder[]>(ref this._intersectionSchemaCache, StringComparer.Ordinal);
			this._dataMemberIdStack = new Stack<string>();
			this._secondaryLeafDataMemberInstanceIds = new List<string>();
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000877C File Offset: 0x0000697C
		private string CurrentDataMemberId
		{
			get
			{
				return this._dataMemberIdStack.Peek();
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000878C File Offset: 0x0000698C
		protected override IList<DataShape> ParseDataShapes(JsonReader reader, JsonSerializer serializer)
		{
			JArray jarray = serializer.Deserialize<JArray>(reader);
			if (jarray != null)
			{
				return jarray.Parse(new Func<JObject, DataShape>(this.ParseDataShape));
			}
			return null;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000087B8 File Offset: 0x000069B8
		private DataShape ParseDataShape(JObject dsObj)
		{
			DataShape dataShape = new DataShape();
			dataShape.Id = (string)dsObj[this.Names.Id];
			dataShape.IsComplete = (bool?)dsObj[this.Names.IsComplete];
			DictionaryEncodingHandler dictionaryEncodingHandler = this.ParseValueDictionaries(dsObj);
			if (this._calcParser == null)
			{
				this._calcParser = new CalculationContainerParser(this.Names, dictionaryEncodingHandler);
			}
			DsrCalculationsBuilder dsrCalculationsBuilder = new DsrCalculationsBuilder();
			dataShape.Calculations = this._calcParser.ParseCalculationsContainer(dsObj, dataShape.Id, dsrCalculationsBuilder);
			dataShape.DataShapes = this.ParseNestedDataShapes(dsObj);
			JToken jtoken = dsObj[this.Names.SecondaryHierarchy];
			if (jtoken != null)
			{
				this._inSecondary = true;
				dataShape.SecondaryHierarchy = jtoken.Parse(new Func<JObject, DataMember>(this.ParseDataMember));
				this._inSecondary = false;
			}
			JToken jtoken2 = dsObj[this.Names.PrimaryHierarchy];
			if (jtoken2 != null)
			{
				dataShape.PrimaryHierarchy = jtoken2.Parse(new Func<JObject, DataMember>(this.ParseDataMember));
			}
			JToken jtoken3 = dsObj[this.Names.DataShapeMessages];
			if (jtoken3 != null)
			{
				dataShape.DataShapeMessages = jtoken3.Parse(new Func<JObject, DataShapeMessage>(this.ParseMessage));
			}
			JToken jtoken4 = dsObj[this.Names.DataLimitsExceeded];
			if (jtoken4 != null)
			{
				dataShape.DataLimitsExceeded = jtoken4.Parse(new Func<JObject, Limit>(this.ParseLimit));
			}
			JToken jtoken5 = dsObj[this.Names.RestartTokens];
			if (jtoken5 != null)
			{
				dataShape.RestartTokens = jtoken5.ToObject<List<RestartToken>>();
			}
			JToken jtoken6 = dsObj[this.Names.HasAllData];
			if (jtoken6 != null)
			{
				dataShape.HasAllData = (bool?)jtoken6;
			}
			JToken jtoken7 = dsObj[this.Names.OdataError];
			if (jtoken7 != null)
			{
				dataShape.Error = jtoken7.ToObject<ODataError>();
			}
			JToken jtoken8 = dsObj[this.Names.DataWindows];
			if (jtoken8 != null)
			{
				dataShape.DataWindows = jtoken8.Parse(new Func<JObject, DataWindow>(this.ParseDataWindow));
			}
			if (dataShape.Calculations == null)
			{
				IList<DataMember> primaryHierarchy = dataShape.PrimaryHierarchy;
			}
			return dataShape;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000089C8 File Offset: 0x00006BC8
		private DictionaryEncodingHandler ParseValueDictionaries(JObject jObj)
		{
			JToken jtoken = jObj[this.Names.ValueDictionaries];
			if (jtoken == null)
			{
				return null;
			}
			DictionaryEncodingHandler dictionaryEncodingHandler = new DictionaryEncodingHandler();
			foreach (JToken jtoken2 in jtoken.Children())
			{
				JProperty jproperty = (JProperty)jtoken2;
				dictionaryEncodingHandler.AddValues(jproperty.Name, jproperty.Value.ToObject<List<object>>());
			}
			return dictionaryEncodingHandler;
		}

		// Token: 0x06000313 RID: 787 RVA: 0x00008A50 File Offset: 0x00006C50
		private List<DataShape> ParseNestedDataShapes(JObject dsContainer)
		{
			JArray jarray = (JArray)dsContainer[this.Names.DataShapes];
			if (jarray == null)
			{
				return null;
			}
			List<DataShape> list = new List<DataShape>(jarray.Count);
			foreach (JToken jtoken in jarray)
			{
				DataShape dataShape = this.ParseDataShape((JObject)jtoken);
				list.Add(dataShape);
			}
			return list;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00008AD0 File Offset: 0x00006CD0
		private Limit ParseLimit(JObject jLimit)
		{
			return new Limit
			{
				Id = (string)jLimit[this.Names.Id]
			};
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00008AF4 File Offset: 0x00006CF4
		private DataShapeMessage ParseMessage(JObject jMessage)
		{
			return new DataShapeMessage
			{
				Code = (string)jMessage[this.Names.CodeUpper],
				Severity = (string)jMessage[this.Names.Severity],
				Message = (string)jMessage[this.Names.MessageUpper],
				ObjectType = (string)jMessage[this.Names.ObjectType],
				ObjectName = (string)jMessage[this.Names.ObjectName],
				PropertyName = (string)jMessage[this.Names.PropertyName]
			};
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00008BB0 File Offset: 0x00006DB0
		private DataWindow ParseDataWindow(JObject jWindow)
		{
			DataWindow dataWindow = new DataWindow();
			dataWindow.Id = (string)jWindow[this.Names.Id];
			dataWindow.IsComplete = (bool)jWindow[this.Names.IsComplete];
			JToken jtoken = jWindow[this.Names.RestartTokens];
			if (jtoken != null)
			{
				dataWindow.RestartTokens = jtoken.ToObject<List<RestartToken>>();
			}
			return dataWindow;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00008C20 File Offset: 0x00006E20
		private DataMember ParseDataMember(JObject jMember)
		{
			DataMember dataMember = new DataMember();
			dataMember.Id = (string)jMember[this.Names.Id];
			JArray jarray = (JArray)jMember[this.Names.Instances];
			if (jarray == null)
			{
				JProperty jproperty = (JProperty)jMember.Children().Single("There should only be one property for a DataMember.", new string[0]);
				if (jproperty != null)
				{
					dataMember.Id = jproperty.Name;
					jarray = (JArray)jproperty.Value;
				}
			}
			dataMember.Instances = this.ParseDataMemberInstances(dataMember.Id, jarray);
			return dataMember;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00008CBC File Offset: 0x00006EBC
		private List<DataMemberInstance> ParseDataMemberInstances(string dataMemberId, JArray jInstances)
		{
			this._dataMemberIdStack.Push(dataMemberId);
			DsrCalculationsBuilder dsrCalculationsBuilder;
			if (!this._dataMemberIdToBuilderCache.TryGetValue(dataMemberId, out dsrCalculationsBuilder))
			{
				dsrCalculationsBuilder = new DsrCalculationsBuilder();
				this._dataMemberIdToBuilderCache.Add(dataMemberId, dsrCalculationsBuilder);
			}
			List<DataMemberInstance> list = new List<DataMemberInstance>(jInstances.Count);
			foreach (JToken jtoken in jInstances)
			{
				list.Add(this.ParseDataMemberInstance((JObject)jtoken, dataMemberId, dsrCalculationsBuilder));
			}
			this._dataMemberIdStack.Pop();
			return list;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00008D5C File Offset: 0x00006F5C
		private DataMemberInstance ParseDataMemberInstance(JObject jInstance, string dataMemberId, DsrCalculationsBuilder dataMemberCalcBuilder)
		{
			DataMemberInstance dataMemberInstance = new DataMemberInstance();
			JToken jtoken = jInstance[this.Names.RestartFlag];
			if (jtoken != null)
			{
				dataMemberInstance.RestartFlag = jtoken.ToObject<RestartFlag>();
			}
			JToken jtoken2 = jInstance[this.Names.RestartKind];
			if (jtoken2 != null)
			{
				dataMemberInstance.RestartKind = jtoken2.ToObject<RestartKind>();
			}
			dataMemberInstance.Calculations = this._calcParser.ParseCalculationsContainer(jInstance, dataMemberId, dataMemberCalcBuilder);
			dataMemberInstance.DataShapes = this.ParseNestedDataShapes(jInstance);
			JToken jtoken3 = jInstance[this.Names.Members];
			if (jtoken3 != null)
			{
				dataMemberInstance.Members = jtoken3.Parse(new Func<JObject, DataMember>(this.ParseDataMember));
			}
			else if (this._inSecondary)
			{
				this._secondaryLeafDataMemberInstanceIds.Add(this.CurrentDataMemberId);
			}
			else
			{
				JToken jtoken4 = jInstance[this.Names.Intersections];
				if (jtoken4 != null)
				{
					dataMemberInstance.Intersections = this.ParseIntersections((JArray)jtoken4);
				}
			}
			return dataMemberInstance;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00008E48 File Offset: 0x00007048
		private List<DataIntersection> ParseIntersections(JArray list)
		{
			string currentDataMemberId = this.CurrentDataMemberId;
			DsrIntersectionCalculationsBuilder[] array;
			if (!this._intersectionSchemaCache.TryGetValue(currentDataMemberId, out array))
			{
				array = new DsrIntersectionCalculationsBuilder[this._secondaryLeafDataMemberInstanceIds.Count];
				this._intersectionSchemaCache[currentDataMemberId] = array;
			}
			List<DataIntersection> list2 = new List<DataIntersection>(array.Length);
			int num = 0;
			foreach (JToken jtoken in list)
			{
				DataIntersection dataIntersection = this.ParseIntersection((JObject)jtoken, ref num, array);
				if (this._fillEmptyIntersections && dataIntersection.Index != null)
				{
					this.FillEmptyIntersections(array, list2, dataIntersection.Index.Value);
					dataIntersection.Index = null;
				}
				list2.Add(dataIntersection);
			}
			if (this._fillEmptyIntersections && list2.Count < array.Length)
			{
				this.FillEmptyIntersections(array, list2, array.Length);
			}
			return list2;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00008F4C File Offset: 0x0000714C
		private void FillEmptyIntersections(DsrIntersectionCalculationsBuilder[] intersectionSchema, List<DataIntersection> newList, int listTargetSize)
		{
			for (int i = newList.Count; i < listTargetSize; i++)
			{
				newList.Add(this.BuildEmptyIntersection(i, intersectionSchema));
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00008F78 File Offset: 0x00007178
		private DataIntersection BuildEmptyIntersection(int logicalIndex, DsrIntersectionCalculationsBuilder[] intersectionSchema)
		{
			string intersectionId = this.GetIntersectionId(logicalIndex);
			return this.GetIntersectionCalcBuilder(intersectionSchema, intersectionId, logicalIndex).EmptyDataIntersection;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00008F9C File Offset: 0x0000719C
		private DataIntersection ParseIntersection(JObject jIntersection, ref int logicalIndex, DsrIntersectionCalculationsBuilder[] intersectionsSchema)
		{
			DataIntersection dataIntersection = new DataIntersection();
			dataIntersection.Id = (string)jIntersection[this.Names.Id];
			JToken jtoken = jIntersection[this.Names.Index];
			if (jtoken != null)
			{
				logicalIndex = (int)jtoken;
				dataIntersection.Index = new int?(logicalIndex);
			}
			if (dataIntersection.Id == null)
			{
				dataIntersection.Id = this.GetIntersectionId(logicalIndex);
			}
			int num = logicalIndex;
			DsrIntersectionCalculationsBuilder dsrIntersectionCalculationsBuilder = this.GetIntersectionCalcBuilder(intersectionsSchema, dataIntersection.Id, num);
			if (dsrIntersectionCalculationsBuilder == null)
			{
				dsrIntersectionCalculationsBuilder = new DsrIntersectionCalculationsBuilder(dataIntersection.Id);
				intersectionsSchema[num] = dsrIntersectionCalculationsBuilder;
				this._intersectionIdToBuilderCache[dataIntersection.Id] = dsrIntersectionCalculationsBuilder;
			}
			dataIntersection.Calculations = this._calcParser.ParseCalculationsContainer(jIntersection, dataIntersection.Id, dsrIntersectionCalculationsBuilder);
			dataIntersection.DataShapes = this.ParseNestedDataShapes(jIntersection);
			logicalIndex++;
			return dataIntersection;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00009070 File Offset: 0x00007270
		private DsrIntersectionCalculationsBuilder GetIntersectionCalcBuilder(DsrIntersectionCalculationsBuilder[] intersectionCalculationsBuilders, string intersectionId, int logicalIndex)
		{
			DsrIntersectionCalculationsBuilder dsrIntersectionCalculationsBuilder = intersectionCalculationsBuilders[logicalIndex];
			if (dsrIntersectionCalculationsBuilder == null)
			{
				if (!this._intersectionIdToBuilderCache.TryGetValue(intersectionId, out dsrIntersectionCalculationsBuilder) && this._fillEmptyIntersections)
				{
					dsrIntersectionCalculationsBuilder = new DsrIntersectionCalculationsBuilder(intersectionId);
					this._intersectionIdToBuilderCache[intersectionId] = dsrIntersectionCalculationsBuilder;
				}
				intersectionCalculationsBuilders[logicalIndex] = dsrIntersectionCalculationsBuilder;
			}
			return dsrIntersectionCalculationsBuilder;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000090B5 File Offset: 0x000072B5
		private string GetIntersectionId(int logicalIndex)
		{
			return this._secondaryLeafDataMemberInstanceIds[logicalIndex] + this.CurrentDataMemberId;
		}

		// Token: 0x040001B4 RID: 436
		private readonly bool _fillEmptyIntersections;

		// Token: 0x040001B5 RID: 437
		private readonly Dictionary<string, DsrCalculationsBuilder> _dataMemberIdToBuilderCache;

		// Token: 0x040001B6 RID: 438
		private readonly Dictionary<string, DsrIntersectionCalculationsBuilder> _intersectionIdToBuilderCache;

		// Token: 0x040001B7 RID: 439
		private readonly Dictionary<string, DsrIntersectionCalculationsBuilder[]> _intersectionSchemaCache;

		// Token: 0x040001B8 RID: 440
		private readonly Stack<string> _dataMemberIdStack;

		// Token: 0x040001B9 RID: 441
		private readonly List<string> _secondaryLeafDataMemberInstanceIds;

		// Token: 0x040001BA RID: 442
		private bool _inSecondary;

		// Token: 0x040001BB RID: 443
		private CalculationContainerParser _calcParser;
	}
}
