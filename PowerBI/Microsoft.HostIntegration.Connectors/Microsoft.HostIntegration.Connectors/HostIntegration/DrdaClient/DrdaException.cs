using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009F9 RID: 2553
	[Serializable]
	public sealed class DrdaException : DbException
	{
		// Token: 0x06004FC4 RID: 20420 RVA: 0x0013FF82 File Offset: 0x0013E182
		internal DrdaException(string errorMessage, string sqlState, int sqlCode)
		{
			this._errMsg = errorMessage;
			this._sqlState = sqlState;
			this._sqlCode = sqlCode;
			this._errorCode = sqlCode;
		}

		// Token: 0x06004FC5 RID: 20421 RVA: 0x0013FFA8 File Offset: 0x0013E1A8
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this._sqlState) && this._sqlCode == 0)
			{
				return this._errMsg;
			}
			return string.Concat(new string[]
			{
				string.IsNullOrEmpty(this._errMsg) ? string.Empty : (this._errMsg + " "),
				"SQLSTATE=",
				this._sqlState,
				" SQLCODE=",
				this._sqlCode.ToString()
			});
		}

		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06004FC6 RID: 20422 RVA: 0x0014002A File Offset: 0x0013E22A
		public string SqlState
		{
			get
			{
				return this._sqlState;
			}
		}

		// Token: 0x17001360 RID: 4960
		// (get) Token: 0x06004FC7 RID: 20423 RVA: 0x00140032 File Offset: 0x0013E232
		public int SqlCode
		{
			get
			{
				return this._sqlCode;
			}
		}

		// Token: 0x17001361 RID: 4961
		// (get) Token: 0x06004FC8 RID: 20424 RVA: 0x0013127E File Offset: 0x0012F47E
		public override string Message
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x17001362 RID: 4962
		// (get) Token: 0x06004FC9 RID: 20425 RVA: 0x0014003A File Offset: 0x0013E23A
		public override int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x06004FCA RID: 20426 RVA: 0x00140044 File Offset: 0x0013E244
		private static string Lookup(string code)
		{
			string @string = DrdaException._resourceManager.GetString(code, CultureInfo.CurrentCulture);
			if (@string != null)
			{
				return @string;
			}
			return string.Empty;
		}

		// Token: 0x06004FCB RID: 20427 RVA: 0x0014006C File Offset: 0x0013E26C
		internal static string Format(string format)
		{
			return DrdaException.Lookup(format);
		}

		// Token: 0x06004FCC RID: 20428 RVA: 0x00140074 File Offset: 0x0013E274
		internal static string Format(string format, object arg0)
		{
			return string.Format(DrdaException.Lookup(format), arg0);
		}

		// Token: 0x06004FCD RID: 20429 RVA: 0x00140082 File Offset: 0x0013E282
		internal static string Format(string format, object arg0, object arg1)
		{
			return string.Format(DrdaException.Lookup(format), arg0, arg1);
		}

		// Token: 0x06004FCE RID: 20430 RVA: 0x00140091 File Offset: 0x0013E291
		internal static string Format(string format, object arg0, object arg1, object arg2)
		{
			return string.Format(DrdaException.Lookup(format), arg0, arg1, arg2);
		}

		// Token: 0x06004FCF RID: 20431 RVA: 0x001400A1 File Offset: 0x0013E2A1
		internal static string Format(string format, object arg0, object arg1, object arg2, object arg3)
		{
			return string.Format(DrdaException.Lookup(format), new object[] { arg0, arg1, arg2, arg3 });
		}

		// Token: 0x06004FD0 RID: 20432 RVA: 0x001400C5 File Offset: 0x0013E2C5
		internal static Exception Argument(string error)
		{
			return new ArgumentException(error);
		}

		// Token: 0x06004FD1 RID: 20433 RVA: 0x001400CD File Offset: 0x0013E2CD
		internal static Exception Argument(string error, Exception inner)
		{
			return new ArgumentException(error, inner);
		}

		// Token: 0x06004FD2 RID: 20434 RVA: 0x001400D6 File Offset: 0x0013E2D6
		internal static Exception ArgumentNull(string parameter)
		{
			return new ArgumentNullException(parameter);
		}

		// Token: 0x06004FD3 RID: 20435 RVA: 0x001400DE File Offset: 0x0013E2DE
		internal static Exception ArgumentNull(string parameter, string error)
		{
			return new ArgumentNullException(parameter, error);
		}

		// Token: 0x06004FD4 RID: 20436 RVA: 0x001400E7 File Offset: 0x0013E2E7
		internal static Exception IndexOutOfRange(string error)
		{
			return new IndexOutOfRangeException(error);
		}

		// Token: 0x06004FD5 RID: 20437 RVA: 0x001400EF File Offset: 0x0013E2EF
		internal static Exception InvalidCast(string error)
		{
			return new InvalidCastException(error);
		}

		// Token: 0x06004FD6 RID: 20438 RVA: 0x001400F7 File Offset: 0x0013E2F7
		internal static Exception InvalidOperation(string error)
		{
			return new InvalidOperationException(error);
		}

		// Token: 0x06004FD7 RID: 20439 RVA: 0x001400FF File Offset: 0x0013E2FF
		internal static Exception NotSupported()
		{
			return new NotSupportedException();
		}

		// Token: 0x06004FD8 RID: 20440 RVA: 0x00140106 File Offset: 0x0013E306
		internal static Exception NotSupported(object o)
		{
			return new NotSupportedException(DrdaException.Format("Drda_NotSupported", o.ToString()));
		}

		// Token: 0x06004FD9 RID: 20441 RVA: 0x0014011D File Offset: 0x0013E31D
		internal static Exception ConnectionTimedOut()
		{
			return new DrdaException(DrdaException.Lookup("Drda_ConnectionTimedOut"), "HY000", -7049);
		}

		// Token: 0x06004FDA RID: 20442 RVA: 0x00140138 File Offset: 0x0013E338
		internal static Exception NoConnectionInPool()
		{
			return new DrdaException(DrdaException.Lookup("Drda_NoConnectionsInPool"), "HY000", -7049);
		}

		// Token: 0x06004FDB RID: 20443 RVA: 0x00140153 File Offset: 0x0013E353
		internal static Exception BatchFailed(int result)
		{
			return new DrdaException(DrdaException.Lookup("Drda_BatchFailed"), null, result);
		}

		// Token: 0x06004FDC RID: 20444 RVA: 0x00140166 File Offset: 0x0013E366
		internal static void CheckArgumentNull(object value, string parameterName)
		{
			if (value == null)
			{
				throw DrdaException.ArgumentNull(parameterName);
			}
		}

		// Token: 0x06004FDD RID: 20445 RVA: 0x00140172 File Offset: 0x0013E372
		internal static Exception InvalidConnectionOptionLength(string key, int maxLength)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_InvalidConnectionOptionLength", key, maxLength));
		}

		// Token: 0x06004FDE RID: 20446 RVA: 0x0014018A File Offset: 0x0013E38A
		internal static Exception InvalidConnectionOptionValue(string key)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_InvalidConnectionOptionValue", key));
		}

		// Token: 0x06004FDF RID: 20447 RVA: 0x0014019C File Offset: 0x0013E39C
		internal static Exception ConvertFailed(Type fromType, Type toType, Exception innerException)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_ConvertFailed", fromType.FullName, toType.FullName), innerException);
		}

		// Token: 0x06004FE0 RID: 20448 RVA: 0x001401BA File Offset: 0x0013E3BA
		internal static Exception CollectionIsNotParent(Type itemType, string propertyName, string propertyValue, Type collection)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_CollectionIsNotParent", itemType.Name, propertyName, propertyValue, collection.Name));
		}

		// Token: 0x06004FE1 RID: 20449 RVA: 0x001401D9 File Offset: 0x0013E3D9
		internal static Exception CollectionIsParent(Type itemType, string propertyName, string propertyValue, Type collection)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_CollectionIsParent", itemType.Name, propertyName, propertyValue, collection.Name));
		}

		// Token: 0x06004FE2 RID: 20450 RVA: 0x001401F8 File Offset: 0x0013E3F8
		internal static Exception CollectionIndexInt32(int index, Type collection, int count)
		{
			return DrdaException.IndexOutOfRange(DrdaException.Format("Drda_CollectionIndexInt32", index.ToString(CultureInfo.CurrentCulture), collection.Name, count.ToString(CultureInfo.CurrentCulture)));
		}

		// Token: 0x06004FE3 RID: 20451 RVA: 0x00140227 File Offset: 0x0013E427
		internal static Exception CollectionIndexString(Type itemType, string propertyName, string propertyValue, Type collection)
		{
			return DrdaException.IndexOutOfRange(DrdaException.Format("Drda_CollectionIndexString", itemType.Name, propertyName, propertyValue, collection.Name));
		}

		// Token: 0x06004FE4 RID: 20452 RVA: 0x00140246 File Offset: 0x0013E446
		internal static Exception CollectionInvalidType(Type collection, Type itemType, object invalidValue)
		{
			return DrdaException.InvalidCast(DrdaException.Format("Drda_CollectionInvalidType", collection.Name, itemType.Name, invalidValue.GetType().Name));
		}

		// Token: 0x06004FE5 RID: 20453 RVA: 0x0014026E File Offset: 0x0013E46E
		internal static Exception CollectionNullValue(string parameter, Type collection, Type itemType)
		{
			return DrdaException.ArgumentNull(parameter, DrdaException.Format("Drda_CollectionNullValue", collection.Name, itemType.Name));
		}

		// Token: 0x06004FE6 RID: 20454 RVA: 0x0014028C File Offset: 0x0013E48C
		internal static Exception NoMixedParameters()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_NoMixedParameters"));
		}

		// Token: 0x06004FE7 RID: 20455 RVA: 0x0014029D File Offset: 0x0013E49D
		internal static Exception ParametersMappingIndex(int index, IDataParameterCollection collection)
		{
			return DrdaException.CollectionIndexInt32(index, collection.GetType(), collection.Count);
		}

		// Token: 0x06004FE8 RID: 20456 RVA: 0x001402B1 File Offset: 0x0013E4B1
		internal static Exception ParametersSourceIndex(string parameterName, IDataParameterCollection collection, Type parameterType)
		{
			return DrdaException.CollectionIndexString(parameterType, DrdaException.ParameterName, parameterName, collection.GetType());
		}

		// Token: 0x06004FE9 RID: 20457 RVA: 0x001402C5 File Offset: 0x0013E4C5
		internal static Exception ParameterNull(string parameter, IDataParameterCollection collection, Type parameterType)
		{
			return DrdaException.CollectionNullValue(parameter, collection.GetType(), parameterType);
		}

		// Token: 0x06004FEA RID: 20458 RVA: 0x001402D4 File Offset: 0x0013E4D4
		internal static Exception InvalidParameterType(IDataParameterCollection collection, Type parameterType, object invalidValue)
		{
			return DrdaException.CollectionInvalidType(collection.GetType(), parameterType, invalidValue);
		}

		// Token: 0x06004FEB RID: 20459 RVA: 0x001402E3 File Offset: 0x0013E4E3
		internal static Exception InvalidParameterValue(Type valueType)
		{
			return new ArgumentException(DrdaException.Format("Drda_InvalidParameterValue", valueType.ToString()));
		}

		// Token: 0x06004FEC RID: 20460 RVA: 0x001402FA File Offset: 0x0013E4FA
		internal static Exception NoLocalTransactionInDistributedContext()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_NoLocalTransactionInDistributedContext"));
		}

		// Token: 0x06004FED RID: 20461 RVA: 0x0014030B File Offset: 0x0013E50B
		internal static Exception CollectionRemoveInvalidObject(Type type, IDataParameterCollection collection)
		{
			return DrdaException.CollectionRemoveInvalidObject(type, collection);
		}

		// Token: 0x06004FEE RID: 20462 RVA: 0x00140314 File Offset: 0x0013E514
		internal static Exception ParametersIsNotParent(Type parameterType, string parameterName, IDataParameterCollection collection)
		{
			return DrdaException.CollectionIsNotParent(parameterType, DrdaException.ParameterName, parameterName, collection.GetType());
		}

		// Token: 0x06004FEF RID: 20463 RVA: 0x00140328 File Offset: 0x0013E528
		internal static Exception ParametersIsParent(Type type, string paramName, IDataParameterCollection collection)
		{
			return DrdaException.CollectionIsParent(type, DrdaException.ParameterName, paramName, collection.GetType());
		}

		// Token: 0x06004FF0 RID: 20464 RVA: 0x0014033C File Offset: 0x0013E53C
		internal static Exception InvalidDrdaType(DrdaClientType msDb2Type)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_InvalidDrdaType", msDb2Type.ToString()));
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x0014035A File Offset: 0x0013E55A
		internal static Exception InvalidDataType(TypeCode typecode)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_InvalidDataType", typecode.ToString("G")));
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x0014037B File Offset: 0x0013E57B
		internal static Exception UnknownDataType(Type dataType)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_UnknownDataType", dataType.FullName));
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x00140392 File Offset: 0x0013E592
		internal static Exception DbTypeNotSupported(DbType type, Type enumtype)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_UnknownDataType", type.ToString("G"), enumtype.Name));
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x001403BC File Offset: 0x0013E5BC
		internal static Exception UnknownDataTypeCode(Type dataType, TypeCode typeCode)
		{
			string text = "Drda_UnknownDataTypeCode";
			int num = (int)typeCode;
			return DrdaException.Argument(DrdaException.Format(text, num.ToString(CultureInfo.CurrentCulture), dataType.FullName));
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x001403EC File Offset: 0x0013E5EC
		internal static Exception NoParallelTransactions()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_ParallelTransactionsNotSupported", typeof(DrdaConnection).Name));
		}

		// Token: 0x06004FF6 RID: 20470 RVA: 0x0014040C File Offset: 0x0013E60C
		internal static Exception AlreadyEndTransaction()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_AlreadyEndTransaction"));
		}

		// Token: 0x06004FF7 RID: 20471 RVA: 0x0014040C File Offset: 0x0013E60C
		internal static Exception TransactionCompleted()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_AlreadyEndTransaction"));
		}

		// Token: 0x06004FF8 RID: 20472 RVA: 0x0014041D File Offset: 0x0013E61D
		internal static Exception NotPartOfTransaction()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_NotPartOfTransaction"));
		}

		// Token: 0x06004FF9 RID: 20473 RVA: 0x0014042E File Offset: 0x0013E62E
		internal static Exception NoActiveTransaction()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_NoActiveTransaction"));
		}

		// Token: 0x06004FFA RID: 20474 RVA: 0x0014043F File Offset: 0x0013E63F
		internal static Exception NoLocalTransactions()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_NoLocalTransactions"));
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x00140450 File Offset: 0x0013E650
		internal static Exception NoDistributedTransactions()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_NoDistributedTransactions"));
		}

		// Token: 0x06004FFC RID: 20476 RVA: 0x00140461 File Offset: 0x0013E661
		internal static Exception NoSchemaCollection(string collection)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_SchemaNoCollection", collection));
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x00140473 File Offset: 0x0013E673
		internal static Exception BadRestrictionType(int restriction, string collection, Type correctType)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_SchemaBadRestrictionType", restriction.ToString(), collection, correctType.ToString()));
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x00140492 File Offset: 0x0013E692
		internal static Exception BadRestrictionLength(int restriction, string collection, int correctLength)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_SchemaBadRestrictionLength", restriction.ToString(), collection, correctLength.ToString()));
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x001404B2 File Offset: 0x0013E6B2
		internal static Exception BadRestrictionCount(string collection, int correctCount)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_SchemaBadRestrictionCount", collection, correctCount.ToString()));
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x001404CB File Offset: 0x0013E6CB
		internal static Exception ClosedConnectionError()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_ClosedConnectionError"));
		}

		// Token: 0x06005001 RID: 20481 RVA: 0x001404DC File Offset: 0x0013E6DC
		internal static Exception ConnectionAlreadyOpen(ConnectionState state)
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_ConnectionAlreadyOpen", state.ToString("G")));
		}

		// Token: 0x06005002 RID: 20482 RVA: 0x001404FD File Offset: 0x0013E6FD
		internal static Exception NoConnectionString()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_NoConnectionString"));
		}

		// Token: 0x06005003 RID: 20483 RVA: 0x0014050E File Offset: 0x0013E70E
		internal static Exception ConnectionStringSyntax(int index)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_ConnectionStringSyntax", index));
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x00140525 File Offset: 0x0013E725
		internal static Exception KeywordNotSupported(string keyword)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_KeywordNotSupported", keyword));
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x00140537 File Offset: 0x0013E737
		internal static Exception UdlFileError(Exception inner)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_UdlFileError"), inner);
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x00140549 File Offset: 0x0013E749
		internal static Exception InvalidUDL()
		{
			return DrdaException.Argument(DrdaException.Format("Drda_InvalidUDL"));
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x0014055A File Offset: 0x0013E75A
		internal static Exception TableListNotSupported()
		{
			return new NotSupportedException(DrdaException.Format("Drda_TableListNotSupported"));
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x0014056C File Offset: 0x0013E76C
		internal static Exception InvalidCommandType(CommandType cmdType)
		{
			string text = "Drda_InvalidCommandType";
			int num = (int)cmdType;
			return DrdaException.Argument(DrdaException.Format(text, num.ToString(CultureInfo.CurrentCulture)));
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x00140596 File Offset: 0x0013E796
		internal static Exception InvalidUpdateRowSource(int rowsrc)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_InvalidUpdateRowSource", rowsrc.ToString(CultureInfo.CurrentCulture)));
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x001405B3 File Offset: 0x0013E7B3
		internal static Exception OpenReaderExists()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_OpenReaderExists"));
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x001405C4 File Offset: 0x0013E7C4
		internal static Exception CommandTextNotSet()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_CommandTextNull"));
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x001405D5 File Offset: 0x0013E7D5
		internal static Exception InvalidCommandTimeout(int value)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_CommandTimeoutInvalid", value.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x001405F2 File Offset: 0x0013E7F2
		internal static Exception InvalidBulkCopyTimeout(int value)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_InvalidBulkCopyTimeoutValue", value.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x0014060F File Offset: 0x0013E80F
		internal static Exception InvalidBulkCopyMappingCollectionTypes()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_InvalidBulkCopyMappingCollectionTypes"));
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x00140620 File Offset: 0x0013E820
		internal static Exception InvalidBulkCopySourceOrdinal(int value)
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_InvalidBulkCopySourceOrdinal", value.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x0014063D File Offset: 0x0013E83D
		internal static Exception InvalidBulkCopyDestinationOrdinal(int value)
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_InvalidBulkCopyDestinationOrdinal", value.ToString(CultureInfo.InvariantCulture)));
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x0014065A File Offset: 0x0013E85A
		internal static Exception InvalidBulkCopyDestinationColumn(string value)
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_InvalidBulkCopyDestinationColumn", value));
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x0014066C File Offset: 0x0013E86C
		internal static Exception InvalidBulkCopySourceColumn(string value)
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_InvalidBulkCopySourceColumn", value));
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x0014067E File Offset: 0x0013E87E
		internal static Exception BulkCopyMDestinationFieldsNotMatch()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_BulkCopyDestinationFieldsNotMatch"));
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x0014068F File Offset: 0x0013E88F
		internal static Exception CommandBatchNoParameters()
		{
			return DrdaException.Argument("Drda_InvalidBatchNoParameters");
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x0014069B File Offset: 0x0013E89B
		internal static Exception CommandBatchValuesToParameters(int countParameters, int countValues)
		{
			return DrdaException.Argument(DrdaException.Format("Drda_InvalidBatchValuesToParameters", countParameters, countValues));
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x001406B8 File Offset: 0x0013E8B8
		internal static Exception DataReaderNoData()
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_DataReaderNoData"));
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x001406C9 File Offset: 0x0013E8C9
		internal static Exception DataReaderClosed(string method)
		{
			return DrdaException.InvalidOperation(DrdaException.Format("Drda_DataReaderClosed", method));
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x001406DB File Offset: 0x0013E8DB
		internal static Exception MakeException(string errorMessage, string sqlState, int sqlCode, int errorCode)
		{
			return new DrdaException(errorMessage, sqlState, sqlCode)
			{
				_errorCode = errorCode
			};
		}

		// Token: 0x04003F21 RID: 16161
		private string _sqlState;

		// Token: 0x04003F22 RID: 16162
		private int _sqlCode;

		// Token: 0x04003F23 RID: 16163
		private string _errMsg;

		// Token: 0x04003F24 RID: 16164
		private int _errorCode;

		// Token: 0x04003F25 RID: 16165
		private static ResourceManager _resourceManager = new ResourceManager("Microsoft.HostIntegration.DrdaClient.Resources", Assembly.GetExecutingAssembly());

		// Token: 0x04003F26 RID: 16166
		private static string ParameterName = "ParameterName";
	}
}
