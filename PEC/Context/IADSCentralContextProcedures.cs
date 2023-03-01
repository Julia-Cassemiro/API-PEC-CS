﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PEC.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace PEC.Context
{
    public partial interface IADSCentralContextProcedures
    {
        Task<List<usp_Medico_Espec_ConvenioChatResult>> usp_Medico_Espec_ConvenioChatAsync(int? idConvenio, int? idEspecialidade, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<List<usp_Separar_Consulta_AgendaChatResult>> usp_Separar_Consulta_AgendaChatAsync(int? idEspecialidade, int? idMedico, DateTime? dtConsulta, int? idConvenio, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}