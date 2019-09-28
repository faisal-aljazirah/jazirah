﻿Imports System.Xml

Public Class s_pharmacy
    Inherits System.Web.UI.Page
    Public DataLang As String
    Dim UserLanguage As Integer
    Dim dcl As New DCL.Conn.DataClassLayer
    Public Save, Cancle, Yes, No As String
    Public intYear, byteLocalCurrency, intStartupFY, SusbendMax, byteCurrencyRound, byteBase, ChangeQuantity_Cash, AddDiscount_Cash, ChangeQuantity_Insurance, AddDiscount_Insurance, AllowExtraItem_Insurance, Auto_MoveRejectedToCash_Insurance, AskBeforeSend, AskBeforeReturn, OnePaymentForCashier, ForcePaymentOnCloseInvoice, lngContact_Doct_Cash, lngContact_Cash, byteDepartment_Cash, lngSalesman_Cash, lngPatient_Cash, DirectCancelInvoic, byteInvoicesLimitDay, byteOrdersLimitDays, DaysToCalculateMedicalInvoices, DaysToCalculateMedicineInvoices, OrdersLimitDays, CancelLimitDays As String
    Public lblintYear, lblbyteLocalCurrency, lblintStartupFY, lblSusbendMax, lblbyteCurrencyRound, lblChangeQuantity_Cash, lblAddDiscount_Cash, lblChangeQuantity_Insurance, lblAddDiscount_Insurance, lblAllowExtraItem_Insurance, lblAuto_MoveRejectedToCash_Insurance, lblAskBeforeSend, lblAskBeforeReturn, lblOnePaymentForCashier, lblForcePaymentOnCloseInvoice, lbllngContact_Doct_Cash, lbllngContact_Cash, lblbyteDepartment_Cash, lbllngSalesman_Cash, lbllngPatient_Cash, lblDirectCancelInvoic, lblbyteInvoicesLimitDay, lblbyteOrdersLimitDays, lblDaysToCalculateMedicalInvoices, lblDaysToCalculateMedicineInvoices, lblOrdersLimitDays, lblCancelLimitDays As String
    Public strContact_Cash, strSalesman_Cash, strPatient_Cash, strDepartment_Cash As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        UserLanguage = Session("UserLanguage")
        'TODO: get data

        loadLanguage()

        Dim doc As New XmlDocument
        doc.Load(HttpContext.Current.Server.MapPath("../data/xml/settings.xml"))
        'get count
        Dim items As String = ""
        Dim application As XmlNode = doc.SelectSingleNode("Settings/Pharmacy")
        intYear = application.SelectSingleNode("intYear").InnerText
        intStartupFY = application.SelectSingleNode("intStartupFY").InnerText
        byteLocalCurrency = application.SelectSingleNode("byteLocalCurrency").InnerText
        byteCurrencyRound = application.SelectSingleNode("byteCurrencyRound").InnerText

        If application.SelectSingleNode("byteDepartment_Cash") Is Nothing Then byteDepartment_Cash = "" Else byteDepartment_Cash = application.SelectSingleNode("byteDepartment_Cash").InnerText
        lngContact_Cash = application.SelectSingleNode("lngContact_Cash").InnerText
        lngSalesman_Cash = application.SelectSingleNode("lngSalesman_Cash").InnerText
        lngPatient_Cash = application.SelectSingleNode("lngPatient_Cash").InnerText

        ChangeQuantity_Cash = application.SelectSingleNode("ChangeQuantity_Cash").InnerText
        ChangeQuantity_Insurance = application.SelectSingleNode("ChangeQuantity_Insurance").InnerText
        AddDiscount_Cash = application.SelectSingleNode("AddDiscount_Cash").InnerText
        AddDiscount_Insurance = application.SelectSingleNode("AddDiscount_Insurance").InnerText
        AllowExtraItem_Insurance = application.SelectSingleNode("AllowExtraItem_Insurance").InnerText
        Auto_MoveRejectedToCash_Insurance = application.SelectSingleNode("Auto_MoveRejectedToCash_Insurance").InnerText
        AskBeforeSend = application.SelectSingleNode("AskBeforeSend").InnerText
        AskBeforeReturn = application.SelectSingleNode("AskBeforeReturn").InnerText
        OnePaymentForCashier = application.SelectSingleNode("OnePaymentForCashier").InnerText
        ForcePaymentOnCloseInvoice = application.SelectSingleNode("ForcePaymentOnCloseInvoice").InnerText
        DirectCancelInvoic = application.SelectSingleNode("DirectCancelInvoic").InnerText
        SusbendMax = application.SelectSingleNode("SusbendMax").InnerText

        byteInvoicesLimitDay = application.SelectSingleNode("byteInvoicesLimitDay").InnerText
        OrdersLimitDays = application.SelectSingleNode("OrdersLimitDays").InnerText
        CancelLimitDays = application.SelectSingleNode("CancelLimitDays").InnerText
        DaysToCalculateMedicalInvoices = application.SelectSingleNode("DaysToCalculateMedicalInvoices").InnerText
        DaysToCalculateMedicineInvoices = application.SelectSingleNode("DaysToCalculateMedicineInvoices").InnerText

        Dim ds As DataSet
        ds = dcl.GetDS("SELECT * FROM Hw_Contacts WHERE lngContact = " & lngContact_Cash & "; SELECT * FROM Hw_Contacts WHERE lngContact = " & lngSalesman_Cash & "; SELECT RTRIM(LTRIM(ISNULL(strFirst" & DataLang & ",'') + ' ') + LTRIM(ISNULL(strSecond" & DataLang & ",'') + ' ') + LTRIM(ISNULL(strThird" & DataLang & " ,'') + ' ') + LTRIM(ISNULL(strLast" & DataLang & ",''))) AS PatientName, * FROM Hw_Patients WHERE lngPatient = " & lngPatient_Cash & "; SELECT * FROM Hw_Departments WHERE byteDepartment = " & byteDepartment_Cash)
        If ds.Tables(0).Rows.Count > 0 Then strContact_Cash = ds.Tables(0).Rows(0).Item("strContact" & DataLang).ToString Else strContact_Cash = ""
        If ds.Tables(1).Rows.Count > 0 Then strSalesman_Cash = ds.Tables(1).Rows(0).Item("strContact" & DataLang).ToString Else strSalesman_Cash = ""
        If ds.Tables(2).Rows.Count > 0 Then strPatient_Cash = ds.Tables(2).Rows(0).Item("PatientName").ToString Else strPatient_Cash = ""
        If ds.Tables(3).Rows.Count > 0 Then strDepartment_Cash = ds.Tables(3).Rows(0).Item("strDepartment" & DataLang).ToString Else strDepartment_Cash = ""
    End Sub
    Sub loadLanguage()
        Select Case UserLanguage
            Case 2
                DataLang = "Ar"
                Title = "لوحة التحكم | الصيدلية"
                '   PageHeader.InnerText = "جديد"
                'Variables
                Yes = "نعم"
                No = "لا"
                Save = "حفظ"
                Cancle = "اغلاق"
                lblintYear = "السنه"
                lblbyteLocalCurrency = "العمله "
                lblintStartupFY = "Start Up FY"
                lblbyteCurrencyRound = "Currency Round "
                lblChangeQuantity_Cash = "Change Quantity Cash"
                lblAddDiscount_Cash = "Add Discount Cash"
                lblChangeQuantity_Insurance = "Change QuantityInsurance"
                lblAddDiscount_Insurance = "Add Discount Insurance"
                lblAllowExtraItem_Insurance = "Allow ExtraItem Insurance"
                lblAuto_MoveRejectedToCash_Insurance = "Move Rejected To Cash Insurance"
                lblSusbendMax = "SusbendMax"
                lblAskBeforeSend = "Ask Before Send"
                lblAskBeforeReturn = "Ask Before Return"
                lblOnePaymentForCashier = "One Payment For Cashier"
                lblForcePaymentOnCloseInvoice = "Force Payment On Close Invoice"
                lbllngContact_Doct_Cash = "lngContact_Doct_Cash"
                lbllngContact_Cash = " شركة التأمين"
                lblbyteDepartment_Cash = "القسم "
                lbllngSalesman_Cash = "الدكتور "
                lbllngPatient_Cash = "المريض"
                lblDirectCancelInvoic = "الغاء فاتوره"
                lblbyteInvoicesLimitDay = "byteInvoicesLimitDay"
                lblbyteOrdersLimitDays = "byteOrdersLimitDays"
                lblDaysToCalculateMedicalInvoices = "DaysToCalculateMedicalInvoices"
                lblDaysToCalculateMedicineInvoices = "DaysToCalculateMedicineInvoices"
                lblOrdersLimitDays = "OrdersLimitDays "
                lblCancelLimitDays = "CancelLimitDays "
                'Columns



            Case Else
                DataLang = "En"
                Title = "Control Panel | Pharmacy"
                ' PageHeader.InnerText = "New"
                'Variables
                Yes = "Yes"
                No = "No"
                Save = "Save"
                Cancle = "Cancle"
                lblintYear = "Year"
                lblbyteLocalCurrency = "Local Currency"
                lblintStartupFY = "Start Up FY"
                lblbyteCurrencyRound = "Currency Round "
                lblChangeQuantity_Cash = "Change Quantity Cash"
                lblAddDiscount_Cash = "Add Discount Cash"
                lblChangeQuantity_Insurance = "Change QuantityInsurance"
                lblAddDiscount_Insurance = "Add Discount Insurance"
                lblAllowExtraItem_Insurance = "Allow ExtraItem Insurance"
                lblAuto_MoveRejectedToCash_Insurance = "Move Rejected To Cash Insurance"
                lblSusbendMax = "SusbendMax"
                lblAskBeforeSend = "Ask Before Send"
                lblAskBeforeReturn = "Ask Before Return"
                lblOnePaymentForCashier = "One Payment For Cashier"
                lblForcePaymentOnCloseInvoice = "Force Payment On Close Invoice"
                lbllngContact_Doct_Cash = "lngContact_Doct_Cash"
                lbllngContact_Cash = "Company"
                lblbyteDepartment_Cash = "Department "
                lbllngSalesman_Cash = " Doctor "
                lbllngPatient_Cash = "Patien"
                lblDirectCancelInvoic = "Cancel Invoic"
                lblbyteInvoicesLimitDay = "byte Invoices Limit Day"
                lblbyteOrdersLimitDays = "byte Orders Limit Days"
                lblDaysToCalculateMedicalInvoices = "DaysToCalculateMedicalInvoices"
                lblDaysToCalculateMedicineInvoices = "DaysToCalculateMedicineInvoices"
                lblOrdersLimitDays = "OrdersLimitDays "
                lblCancelLimitDays = "CancelLimitDays "
                'Columns


        End Select
    End Sub

    <System.Web.Services.WebMethod()>
    Public Shared Function findDepartment(ByVal query As String) As String
        Dim str As New StringBuilder("")
        Dim filter As String
        Dim s_PadLetter As String = ""
        Dim s_Padding As Integer = 0
        Dim s_SerialID As Boolean = True
        If filter <> "" Then filter = " AND " & filter
        str.Append("{""suggestions"": [ ")
        Dim myds As New DataSet
        Dim dcl As New DCL.Conn.DataClassLayer
        Dim DataLang As String = "En"
        myds = dcl.GetDS("SELECT TOP 5 * FROM Hw_Departments WHERE strDepartment" & DataLang & " LIKE '%" & query & "%'" & filter)
        For I = 0 To myds.Tables(0).Rows.Count - 1
            str.Append("{ ""value"": """ & myds.Tables(0).Rows(I).Item("strDepartment" & DataLang).ToString & """, ""id"": """ & myds.Tables(0).Rows(I).Item("byteDepartment").ToString & """ },")
        Next
        str.Remove(str.Length - 1, 1)
        str.Append(" ]}")
        Return str.ToString
    End Function



    <System.Web.Services.WebMethod()>
    Public Shared Function findContactDoct(ByVal query As String) As String
        Dim str As New StringBuilder("")
        Dim filter As String
        Dim s_PadLetter As String = ""
        Dim s_Padding As Integer = 0
        Dim s_SerialID As Boolean = True
        If filter <> "" Then filter = " AND " & filter
        str.Append("{""suggestions"": [ ")
        Dim myds As New DataSet
        Dim dcl As New DCL.Conn.DataClassLayer
        Dim DataLang As String = "En"
        myds = dcl.GetDS("SELECT TOP 5 * FROM Hw_Contacts WHERE byteClass=3 and strContact" & DataLang & " LIKE '%" & query & "%'" & filter)
        For I = 0 To myds.Tables(0).Rows.Count - 1
            str.Append("{ ""value"": """ & myds.Tables(0).Rows(I).Item("strContact" & DataLang).ToString & """, ""id"": """ & myds.Tables(0).Rows(I).Item("lngContact").ToString & """ },")
        Next
        str.Remove(str.Length - 1, 1)
        str.Append(" ]}")
        Return str.ToString
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function findContactComp(ByVal query As String) As String
        Dim str As New StringBuilder("")
        Dim filter As String
        Dim s_PadLetter As String = ""
        Dim s_Padding As Integer = 0
        Dim s_SerialID As Boolean = True
        If filter <> "" Then filter = " AND " & filter
        str.Append("{""suggestions"": [ ")
        Dim myds As New DataSet
        Dim dcl As New DCL.Conn.DataClassLayer
        Dim DataLang As String = "En"
        myds = dcl.GetDS("SELECT TOP 5 * FROM Hw_Contacts WHERE byteType=1 and strContact" & DataLang & " LIKE '%" & query & "%'" & filter)
        For I = 0 To myds.Tables(0).Rows.Count - 1
            str.Append("{ ""value"": """ & myds.Tables(0).Rows(I).Item("strContact" & DataLang).ToString & """, ""id"": """ & myds.Tables(0).Rows(I).Item("lngContact").ToString & """ },")
        Next
        str.Remove(str.Length - 1, 1)
        str.Append(" ]}")
        Return str.ToString
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function findPatient(ByVal query As String) As String
        Dim str As New StringBuilder("")
        Dim filter As String
        Dim s_PadLetter As String = ""
        Dim s_Padding As Integer = 0
        Dim s_SerialID As Boolean = True
        If filter <> "" Then filter = " AND " & filter
        str.Append("{""suggestions"": [ ")
        Dim myds As New DataSet
        Dim dcl As New DCL.Conn.DataClassLayer
        Dim DataLang As String = "En"
        myds = dcl.GetDS("SELECT TOP 5 lngPatient, ISNULL(strFirst" & DataLang & ", '')   + ' ' +  ISNULL(strSecond" & DataLang & ", '')  + ' ' + ISNULL(strThird" & DataLang & ", '' )  + ' ' + ISNULL(strLast" & DataLang & ", '' ) as FullName FROM Hw_Patients WHERE (ISNULL(strFirst" & DataLang & ", '')   + ' ' +  ISNULL(strSecond" & DataLang & ", '')  + ' ' + ISNULL(strThird" & DataLang & ", '' )  + ' ' + ISNULL(strLast" & DataLang & ", '' )) LIKE '%" & query & "%'" & filter)
        For I = 0 To myds.Tables(0).Rows.Count - 1
            str.Append("{ ""value"": """ & myds.Tables(0).Rows(I).Item("FullName").ToString & """, ""id"": """ & myds.Tables(0).Rows(I).Item("lngPatient").ToString & """ },")
        Next
        str.Remove(str.Length - 1, 1)
        str.Append(" ]}")
        Return str.ToString
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function saveGeneralSettings(ByVal intYear As String, ByVal intStartupFY As String, ByVal byteLocalCurrency As String, ByVal byteCurrencyRound As String) As String
        Try
            Dim doc As New XmlDocument
            doc.Load(HttpContext.Current.Server.MapPath("../data/xml/settings.xml"))
            Dim application As XmlNode = doc.SelectSingleNode("Settings/Pharmacy")
            If application.SelectSingleNode("intYear") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("intYear")
                node.InnerText = intYear
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("intYear").InnerText = intYear
            End If
            If application.SelectSingleNode("intStartupFY") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("intStartupFY")
                node.InnerText = intStartupFY
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("intStartupFY").InnerText = intStartupFY
            End If
            If application.SelectSingleNode("byteLocalCurrency") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("byteLocalCurrency")
                node.InnerText = byteLocalCurrency
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("byteLocalCurrency").InnerText = byteLocalCurrency
            End If
            If application.SelectSingleNode("byteCurrencyRound") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("byteCurrencyRound")
                node.InnerText = byteCurrencyRound
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("byteCurrencyRound").InnerText = byteCurrencyRound
            End If
            doc.Save(HttpContext.Current.Server.MapPath("../data/xml/settings.xml"))
            Return ""
        Catch ex As Exception
            Return "Err:" & ex.Message
        End Try
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function saveDefaultCash(ByVal byteDepartment_Cash As String, ByVal lngContact_Cash As String, ByVal lngSalesman_Cash As String, ByVal lngPatient_Cash As String) As String
        Try
            Dim doc As New XmlDocument
            doc.Load(HttpContext.Current.Server.MapPath("../data/xml/settings.xml"))
            Dim application As XmlNode = doc.SelectSingleNode("Settings/Pharmacy")
            If application.SelectSingleNode("byteDepartment_Cash") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("byteDepartment_Cash")
                node.InnerText = byteDepartment_Cash
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("byteDepartment_Cash").InnerText = byteDepartment_Cash
            End If
            If application.SelectSingleNode("lngContact_Cash") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("lngContact_Cash")
                node.InnerText = lngContact_Cash
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("lngContact_Cash").InnerText = lngContact_Cash
            End If
            If application.SelectSingleNode("lngSalesman_Cash") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("lngSalesman_Cash")
                node.InnerText = lngSalesman_Cash
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("lngSalesman_Cash").InnerText = lngSalesman_Cash
            End If
            If application.SelectSingleNode("lngPatient_Cash") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("lngPatient_Cash")
                node.InnerText = lngPatient_Cash
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("lngPatient_Cash").InnerText = lngPatient_Cash
            End If
            doc.Save(HttpContext.Current.Server.MapPath("../data/xml/settings.xml"))
            Return ""
        Catch ex As Exception
            Return "Err:" & ex.Message
        End Try
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function saveRestrictions(ByVal ChangeQuantity_Cash As String, ByVal ChangeQuantity_Insurance As String, ByVal AddDiscount_Cash As String, ByVal AddDiscount_Insurance As String, ByVal AllowExtraItem_Insurance As String, ByVal Auto_MoveRejectedToCash_Insurance As String, ByVal AskBeforeSend As String, ByVal AskBeforeReturn As String, ByVal OnePaymentForCashier As String, ByVal ForcePaymentOnCloseInvoice As String, ByVal DirectCancelInvoic As String, ByVal SusbendMax As String) As String
        Try
            Dim doc As New XmlDocument
            doc.Load(HttpContext.Current.Server.MapPath("../data/xml/settings.xml"))
            Dim application As XmlNode = doc.SelectSingleNode("Settings/Pharmacy")
            If application.SelectSingleNode("ChangeQuantity_Cash") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("ChangeQuantity_Cash")
                node.InnerText = CBool(ChangeQuantity_Cash)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("ChangeQuantity_Cash").InnerText = CBool(ChangeQuantity_Cash)
            End If
            If application.SelectSingleNode("ChangeQuantity_Insurance") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("ChangeQuantity_Insurance")
                node.InnerText = CBool(ChangeQuantity_Insurance)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("ChangeQuantity_Insurance").InnerText = CBool(ChangeQuantity_Insurance)
            End If
            If application.SelectSingleNode("AddDiscount_Cash") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("AddDiscount_Cash")
                node.InnerText = CBool(AddDiscount_Cash)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("AddDiscount_Cash").InnerText = CBool(AddDiscount_Cash)
            End If
            If application.SelectSingleNode("AddDiscount_Insurance") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("AddDiscount_Insurance")
                node.InnerText = CBool(AddDiscount_Insurance)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("AddDiscount_Insurance").InnerText = CBool(AddDiscount_Insurance)
            End If

            If application.SelectSingleNode("AllowExtraItem_Insurance") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("AllowExtraItem_Insurance")
                node.InnerText = CBool(AllowExtraItem_Insurance)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("AllowExtraItem_Insurance").InnerText = CBool(AllowExtraItem_Insurance)
            End If
            If application.SelectSingleNode("Auto_MoveRejectedToCash_Insurance") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("Auto_MoveRejectedToCash_Insurance")
                node.InnerText = CBool(Auto_MoveRejectedToCash_Insurance)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("Auto_MoveRejectedToCash_Insurance").InnerText = CBool(Auto_MoveRejectedToCash_Insurance)
            End If
            If application.SelectSingleNode("AskBeforeSend") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("AskBeforeSend")
                node.InnerText = CBool(AskBeforeSend)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("AskBeforeSend").InnerText = CBool(AskBeforeSend)
            End If
            If application.SelectSingleNode("AskBeforeReturn") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("AskBeforeReturn")
                node.InnerText = CBool(AskBeforeReturn)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("AskBeforeReturn").InnerText = CBool(AskBeforeReturn)
            End If
            If application.SelectSingleNode("OnePaymentForCashier") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("OnePaymentForCashier")
                node.InnerText = CBool(OnePaymentForCashier)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("OnePaymentForCashier").InnerText = CBool(OnePaymentForCashier)
            End If
            If application.SelectSingleNode("ForcePaymentOnCloseInvoice") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("ForcePaymentOnCloseInvoice")
                node.InnerText = CBool(ForcePaymentOnCloseInvoice)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("ForcePaymentOnCloseInvoice").InnerText = CBool(ForcePaymentOnCloseInvoice)
            End If
            If application.SelectSingleNode("DirectCancelInvoic") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("DirectCancelInvoic")
                node.InnerText = CBool(DirectCancelInvoic)
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("DirectCancelInvoic").InnerText = CBool(DirectCancelInvoic)
            End If
            If application.SelectSingleNode("SusbendMax") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("SusbendMax")
                node.InnerText = SusbendMax
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("SusbendMax").InnerText = SusbendMax
            End If
            doc.Save(HttpContext.Current.Server.MapPath("../data/xml/settings.xml"))
            Return ""
        Catch ex As Exception
            Return "Err:" & ex.Message
        End Try
    End Function

    <System.Web.Services.WebMethod()>
    Public Shared Function saveLimitations(ByVal byteInvoicesLimitDay As String, ByVal OrdersLimitDays As String, ByVal CancelLimitDays As String, ByVal DaysToCalculateMedicalInvoices As String, ByVal DaysToCalculateMedicineInvoices As String) As String
        Try
            Dim doc As New XmlDocument
            doc.Load(HttpContext.Current.Server.MapPath("../data/xml/settings.xml"))
            Dim application As XmlNode = doc.SelectSingleNode("Settings/Pharmacy")
            If application.SelectSingleNode("byteInvoicesLimitDay") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("byteInvoicesLimitDay")
                node.InnerText = byteInvoicesLimitDay
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("byteInvoicesLimitDay").InnerText = byteInvoicesLimitDay
            End If
            If application.SelectSingleNode("OrdersLimitDays") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("OrdersLimitDays")
                node.InnerText = OrdersLimitDays
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("OrdersLimitDays").InnerText = OrdersLimitDays
            End If
            If application.SelectSingleNode("CancelLimitDays") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("CancelLimitDays")
                node.InnerText = CancelLimitDays
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("CancelLimitDays").InnerText = CancelLimitDays
            End If
            If application.SelectSingleNode("DaysToCalculateMedicalInvoices") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("DaysToCalculateMedicalInvoices")
                node.InnerText = DaysToCalculateMedicalInvoices
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("DaysToCalculateMedicalInvoices").InnerText = DaysToCalculateMedicalInvoices
            End If
            If application.SelectSingleNode("DaysToCalculateMedicineInvoices") Is Nothing Then
                Dim node As XmlElement = doc.CreateElement("DaysToCalculateMedicineInvoices")
                node.InnerText = DaysToCalculateMedicineInvoices
                doc.DocumentElement.AppendChild(node)
            Else
                application.SelectSingleNode("DaysToCalculateMedicineInvoices").InnerText = DaysToCalculateMedicineInvoices
            End If
            doc.Save(HttpContext.Current.Server.MapPath("../data/xml/settings.xml"))
            Return ""
        Catch ex As Exception
            Return "Err:" & ex.Message
        End Try
    End Function
End Class