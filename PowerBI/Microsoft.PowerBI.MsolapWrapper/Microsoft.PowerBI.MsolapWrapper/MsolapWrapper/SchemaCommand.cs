using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace MsolapWrapper
{
	// Token: 0x02000082 RID: 130
	public class SchemaCommand : IDisposable
	{
		// Token: 0x060001C4 RID: 452 RVA: 0x00009118 File Offset: 0x00008518
		internal SchemaCommand(Connection connection)
		{
			string text = "The connection should be opened before creating a command on it.";
			if (!connection.IsOpen())
			{
				WrapperContract.Fail(text);
			}
			this._connection = connection;
			this._propSets = new SchemaCommandPropertySetCollection();
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000083B4 File Offset: 0x000077B4
		internal void AddProperty(SchemaCommandProperties property, object value)
		{
			this._propSets.AddProperty(property, value);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008A74 File Offset: 0x00007E74
		internal string GetModelMetadata(object[] restrictions)
		{
			DataReader dataReader = this.ExecuteReader(SchemaGuid.Csdl, restrictions);
			if (!dataReader.MoveNext())
			{
				dataReader.Close();
				if (dataReader != null)
				{
					((IDisposable)dataReader).Dispose();
				}
				return null;
			}
			object value = dataReader.GetValue(0U);
			if (value == null)
			{
				WrapperContract.Fail("Expected to have schema contents.");
			}
			string text = "Schema contents are expected to be a string.";
			if (!(value as string != null))
			{
				WrapperContract.Fail(text);
			}
			dataReader.Close();
			if (dataReader != null)
			{
				((IDisposable)dataReader).Dispose();
			}
			return SchemaCommandUtils.RewriteCsdlNamespace((string)value);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008420 File Offset: 0x00007820
		internal DataReader ExecuteReader(Guid schema, object[] restrictions)
		{
			CDbSchemaRowsetWrapper cdbSchemaRowsetWrapper = new CDbSchemaRowsetWrapper(this._connection);
			this._schemaRowset = cdbSchemaRowsetWrapper;
			cdbSchemaRowsetWrapper.ExecuteSchemaRowset(schema, restrictions, this._propSets);
			return new DataReader(this._schemaRowset, true);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000083D4 File Offset: 0x000077D4
		internal long GetSchemasRestrictionsCount(Guid schema)
		{
			CDbSchemaRowsetWrapper cdbSchemaRowsetWrapper = new CDbSchemaRowsetWrapper(this._connection);
			double schemaRestrictionMask = (double)cdbSchemaRowsetWrapper.GetSchemaRestrictionMask(schema);
			cdbSchemaRowsetWrapper.Close();
			if (cdbSchemaRowsetWrapper != null)
			{
				((IDisposable)cdbSchemaRowsetWrapper).Dispose();
			}
			long num = <Module>.log2(schemaRestrictionMask + 1.0);
			GC.KeepAlive(this);
			return num;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008460 File Offset: 0x00007860
		internal void Close()
		{
			CDbSchemaRowsetWrapper schemaRowset = this._schemaRowset;
			if (schemaRowset != null)
			{
				schemaRowset.Close();
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008484 File Offset: 0x00007884
		private void !SchemaCommand()
		{
			CDbSchemaRowsetWrapper schemaRowset = this._schemaRowset;
			if (schemaRowset != null)
			{
				((IDisposable)schemaRowset).Dispose();
				this._schemaRowset = null;
			}
			SchemaCommandPropertySetCollection propSets = this._propSets;
			if (propSets != null)
			{
				((IDisposable)propSets).Dispose();
				this._propSets = null;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000084C8 File Offset: 0x000078C8
		private void ~SchemaCommand()
		{
			this.Close();
			this.!SchemaCommand();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00005FCC File Offset: 0x000053CC
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~SchemaCommand();
			}
			else
			{
				try
				{
					this.!SchemaCommand();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000064F8 File Offset: 0x000058F8
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00006018 File Offset: 0x00005418
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x0400020C RID: 524
		private Connection _connection;

		// Token: 0x0400020D RID: 525
		private CDbSchemaRowsetWrapper _schemaRowset;

		// Token: 0x0400020E RID: 526
		private SchemaCommandPropertySetCollection _propSets;
	}
}
