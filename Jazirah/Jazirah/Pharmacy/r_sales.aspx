﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Pharmacy/PH.Master" CodeBehind="r_sales.aspx.vb" Inherits="Jazirah.r_sales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <link rel="stylesheet" type="text/css" href="../app-assets/vendors/css/datatables/dataTables.bootstrap4.css">
    <link rel="stylesheet" type="text/css" href="../app-assets/vendors/css/datatables/buttons.bootstrap4.css">
    <link rel="stylesheet" type="text/css" href="../app-assets/vendors/css/daterangepicker/daterangepicker.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row">
    <div class="col-xs-10">
        <div class="card">
            <div class="card-body">
                <div class="" style="padding:10px" id="divReportButtons">
					
			    </div>
            </div>
        </div>
    </div>
    <div class="col-xs-2">
        <div class="card">
            <div class="card-body">
                <div class="" style="padding:10px">
					<button type="button" class="btn btn-outline-deep-orange btn-sm full-width" onclick="javascript:filterReport(filter)" disabled="disabled"><%=btnFilter%></button>
			    </div>
            </div>
        </div>
    </div>
</div>
<div class="row" id="tblSalesReport">

</div>
<iframe id="txtArea1" style="display:none"></iframe>
<div id="prtJS"></div>
<div class="modal fade text-xs-left" id="mdlFilter" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true"></div>
<div class="modal fade" id="mdlInfo" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true" data-backdrop="false"></div>
<div class="modal fade" id="mdlMessage" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true" data-backdrop="false"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <script src="../app-assets/vendors/js/datatables/jquery.dataTables.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/datatables/dataTables.bootstrap4.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/datatables/dataTables.buttons.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/datatables/buttons.bootstrap4.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/datatables/buttons.colVis.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/datatables/buttons.flash.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/datatables/buttons.html5.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/datatables/buttons.print.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/autocomplete/jquery.autocomplete.min.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/moment/moment.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/moment/moment-with-locales.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/daterangepicker/daterangepicker.min.js" type="text/javascript"></script>
  <script src="../app-assets/vendors/js/forms/extended/formatter/formatter.min.js" type="text/javascript"></script>
  <script type="text/javascript">
      //moment.locale('ar-sa');
      //dom = 'Bfrtip';
      //buttons = "[{extend: 'print',text: 'Print current page',autoPrint: false}]";
      //buttons = "['print']";
      function filterReport(filter) {
          $.ajax({
              type: 'POST',
              url: 'ajax.aspx/filterReport',
              data: '{Source: "Sales", Filter: "' + filter + '"}',
              contentType: 'application/json; charset=utf-8',
              dataType: 'json',
              success: function (response) {
                  if (response.d.substr(0, 4) == 'Err:') {
                      msg('', response.d.substr(4, response.d.length), 'error');
                  } else {
                      $('#mdlFilter').html(response.d);
                      $('#mdlFilter').modal('show');
                  }
              },
              failure: function (msg) {
                  alert(msg);
              },
              error: function (xhr, ajaxOptions, thrownError) {
                  alert("Load Form, update form error! " + xhr.status + " error =" + thrownError + " xhr.responseText = " + xhr.responseText);
              }
          });
      }
      
      $(document).ready(function () {
          if ($("body").hasClass("rtl")) {
              btnCopy = 'نسخ';
              btnExcel = 'إكسل';
              btnPrint = 'طباعة';
              btnColumns = 'الأعمدة';
          } else {
              btnCopy = 'Copy';
              btnExcel = 'Excel';
              btnPrint = 'Print';
              btnColumns = 'Columns';
          }
          //def = [{ "visible": false, "targets": [<%=selectedColumns%>] }];
          def = [{ "visible": false, "targets": [] }];
          dom = "<'row'<'col-sm-12 col-md-4'l><'col-sm-12 col-md-4 text-md-center'B><'col-sm-12 col-md-4'f>><'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>";
          buttons = [
              {
                  extend: 'copy',
                  text: btnCopy,
                  className: 'btn btn-secondry btn-sm',
                  exportOptions: {
                      columns: ':visible'
                  }
              }, {
                  extend: 'csv',
                  text: btnExcel,
                  className: 'btn btn-secondry btn-sm',
                  exportOptions: {
                      columns: ':visible'
                  }
              }, {
                  extend: 'print',
                  title: '',
                  footer: true,
                  messageTop: '',
                  text: btnPrint,
                  autoPrint: false,
                  className: 'btn btn-secondry btn-sm',
                  exportOptions: {
                      columns: ':visible'
                  },
                  customize: function (win) {
                      $(win.document.body)
                          .css('font-size', '10pt')
                          .prepend(
                              '<h1>Aljazirah</h1>'
                          );

                      $(win.document.body).find('table')
                          .addClass('compact')
                          .css('font-size', 'inherit');
                      //$(win.document.body).find('td').html('found');
                  }
              }, {
                  extend: 'colvis',
                  text: btnColumns,
                  className: 'btn btn-secondry btn-sm',
                  columns: ':gt(0)'
              }, {
                  text: 'Refresh',
                  className: 'btn btn-secondry btn-sm',
                  action: function (e, dt, node, config) {
                      alert('Button activated');
                  }
              }
          ];
      });

      //$('.table').DataTable({
      //    dom: "<'row'<'col-sm-12 col-md-4'l><'col-sm-12 col-md-4 text-md-center'B><'col-sm-12 col-md-4'f>><'row'<'col-sm-12'tr>><'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>",
      //    buttons: ['copy', 'csv', { extend: 'print', text: 'Print', autoPrint: false, className: 'btn btn-secondry btn-sm', exportOptions: { columns: ':visible' } }, { extend: 'colvis', text: 'Columns', className: 'btn btn-secondry btn-sm', columns: ':gt(0)' }]
      //});

      var filter = '<%=strFilter%>';
      var report = {
          reportname: 'Standard Report',
          createuser: 'faisal',
          createdate: '2019-11-14',
          filter: {
              invoice: {
                  datetype: 1,
                  datefrom: '2019-12-06',
                  dateto: '2019-12-06',
                  numbertype: 1,
                  numberfactor: '',
                  numbervalue: '',
                  invoicetype: 2,
                  invoicestatus: [1,2]
              },
              department: {},
              doctor: {},
              company: {
                  compnayid: []
              },
              user: {}
          },
          columns: [1, 2, 12, 7, 8, 9, 10, 17, 18],
          result: {
              grouping: [11],
              sorting: [{ col: 0, sort: 'asc' }],
              viewing: 1
          }
      };

      var report3 = {
          reportname: 'Standard Report',
          createuser: 'faisal',
          createdate: '2019-11-14',
          filter: {
              invoice: {
                  datetype: 1,
                  datefrom: '2019-12-01',
                  dateto: '2019-12-31',
                  numbertype: 1,
                  numberfactor: '',
                  numbervalue: '',
                  invoicetype: 2,
                  invoicestatus: [1, 2]
              },
              department: {},
              doctor: {},
              company: {
                  compnayid: []
              },
              user: {}
          },
          columns: [9, 10, 15, 17, 21, 22, 23],
          result: {
              grouping: [],
              sorting: [{ col: 0, sort: 'asc' }],
              viewing: 2
          }
      };

      var report2 = {
          reportname: 'Standard Report',
          createuser: 'faisal',
          createdate: '2019-11-14',
          filter: {
              invoice: {
                  datetype: 1,
                  datefrom: '2019-10-27',
                  dateto: '2019-11-27',
                  numbertype: 1,
                  numberfactor: '',
                  numbervalue: '',
                  invoicetype: 1,
                  invoicestatus: [1, 2]
              },
              department: {},
              doctor: {},
              company: {
                  compnayid: [380]
              },
              user: {}
          },
          columns: [1, 2, 12, 7, 8, 9, 10, 15, 16, 17, 18],
          result: {
              grouping: [11],
              sorting: [{ col: 0, sort: 'asc' }],
              viewing: 1
          }
      };

      function fillSalesReport(f) {
          $('#tblSalesReport').html('<div class="bg-grey text-md-center text-bold-300 bg-lighten-4 pt-2 pb-2"><img src="../app-assets/images/icons/spinner.gif" /> <%=strWait %></div>');
          $.ajax({
              type: 'POST',
              url: 'ajax.aspx/fillSalesReport',
              data: '{Filter: "' + f + '"}',
              contentType: 'application/json; charset=utf-8',
              dataType: 'json',
              success: function (response) {
                  if (response.d.substr(0, 4) == 'Err:') {
                      msg('', response.d.substr(4, response.d.length), 'error')
                  } else {
                      $('#tblSalesReport').html(response.d);
                  }
              },
              failure: function (msg) {
                  alert(msg);
              },
              error: function (xhr, ajaxOptions, thrownError) {
                  alert("Load Form, update form error! " + xhr.status + " error =" + thrownError + " xhr.responseText = " + xhr.responseText);
              }
          });
      }

      function fillReportButtons() {
                $.ajax({
                    type: 'POST',
                    url: 'ajax.aspx/fillReportButtons',
                    data: '{}',
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        if (response.d.substr(0, 4) == 'Err:') {
                            msg('', response.d.substr(4, response.d.length), 'error')
                        } else {
                            $('#divReportButtons').html(response.d);
                        }
                    },
                    failure: function (msg) {
                        alert(msg);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert("Load Form, update form error! " + xhr.status + " error =" + thrownError + " xhr.responseText = " + xhr.responseText);
                    }
                });
            }

      function getSalesReport(r) {
          var valJson = JSON.stringify(r);
          var dataJson = { Report: valJson };
          var dataJsonString = JSON.stringify(dataJson);
          //var datajson = {"Report": r};
          $('#tblSalesReport').html('<div class="bg-grey text-md-center text-bold-300 bg-lighten-4 pt-2 pb-2"><img src="../app-assets/images/icons/spinner.gif" /> <%=strWait %></div>');
                $.ajax({
                    type: 'POST',
                    url: 'ajax.aspx/getSalesReport',
                    data: dataJsonString,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    success: function (response) {
                        if (response.d.substr(0, 4) == 'Err:') {
                            msg('', response.d.substr(4, response.d.length), 'error')
                        } else {
                            $('#tblSalesReport').html(response.d);
                        }
                    },
                    failure: function (msg) {
                        alert(msg);
                    },
                    error: function (xhr, ajaxOptions, thrownError) {
                        alert("Load Form, update form error! " + xhr.status + " error =" + thrownError + " xhr.responseText = " + xhr.responseText);
                    }
                });
            }

      var dateFormat = '<%=strDateFormat%>';
      var datePattern = dateFormat.replace('yyyy', '{{9999}}').replace('MM', '{{99}}').replace('dd', '{{99}}');

      //fillSalesReport(filter);

      //getSalesReport(report);
      fillReportButtons();

      function generateReport(func, options) {
          var valJson = JSON.stringify(options);
          //var dataJson = { Options: valJson };
          var dataJsonString = JSON.stringify(valJson);
          $.ajax({
              type: 'POST',
              url: 'ajax.aspx/generateReport',
              data: '{Func: "' + func + '", Options: ' + dataJsonString + '}',
              contentType: 'application/json; charset=utf-8',
              dataType: 'json',
              success: function (response) {
                  if (response.d.substr(0, 4) == 'Err:') {
                      msg('', response.d.substr(4, response.d.length), 'error')
                  } else {
                      $('#mdlMessage').html(response.d);
                      $('#mdlMessage').modal('show');
                  }
              },
              failure: function (msg) {
                  alert(msg);
              },
              error: function (xhr, ajaxOptions, thrownError) {
                  alert("Load Form, update form error! " + xhr.status + " error =" + thrownError + " xhr.responseText = " + xhr.responseText);
              }
          });
      }

      function printReport() {
          var table = $('#tblSalesReport table');
          var content = table.html();
          
          var head = '<head><title>Report</title><style>table {width:100%;font-size:12px; border-right: #000 solid 1px; border-bottom: #000 solid 1px; border-collapse:collapse;} table td,table th{border: #000 solid 1px; padding: 4px;} .head {background-color:#CCC;} .group {border:none;} .groupname {border:#000 solid 1px; border-radius: 10px 10px;background-color:#CCC; padding-left: 5px; padding-right: 5px;} .subtotal {background-color:#CCC;}</style></head>';
          //content.find('.group').each(function () { $(this).html('<td>Ha</td>'); });
          content = content.replace(/class="head"/g, 'style="background-color:#808080;"');
          content = content.replace(/<h3>/g, '<div class="groupname"><h3>');
          content = content.replace(/<\/h3>/g, '</h3></div');
          var w = window.open();
          w.document.write(head + '<body><table>' + content + '</table></body>');

          w.document.write('<script type="text/javascript">setTimeout(function () {window.print();window.close();}, 1000); <\/script>');

          w.document.close();
      }

      function exportReport() {
          var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
          var textRange; var j = 0;
          tab = document.getElementById('tblReport'); // id of table
          
          for (j = 0 ; j < tab.rows.length ; j++) {
              tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
              //tab_text=tab_text+"</tr>";
          }

          tab_text = tab_text + "</table>";
          tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
          tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
          tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

          var ua = window.navigator.userAgent;
          var msie = ua.indexOf("MSIE ");

          if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
          {
              txtArea1.document.open("txt/html", "replace");
              txtArea1.document.write(tab_text);
              txtArea1.document.close();
              txtArea1.focus();
              sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
          }
          else                 //other browser not tested on IE 11
              sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

          return (sa);
      }
	</script>
</asp:Content>
