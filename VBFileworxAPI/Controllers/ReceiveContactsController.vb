Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Web.Http
Imports FileworxObjectClassLibrary
Imports FileworxObjectClassLibrary.Models
Imports FileworxObjectClassLibrary.Queries

Namespace Controllers

    Public Class ReceiveContactsController
        Inherits ApiController

        Public Async Function GetValues() As Task(Of HttpResponseMessage)
            Try
                Dim query As New clsContactQuery()
                query.QDirection = New ContactDirection() {ContactDirection.Receive, (ContactDirection.Transmit Or ContactDirection.Receive)}
                query.Source = QuerySource.DB

                Dim receiveContacts = Await query.RunAsync()

                ' Return a 200 OK response with data (replace "receiveContacts" with your actual data)
                Return Request.CreateResponse(HttpStatusCode.OK, receiveContacts)

            Catch ex As Exception
                ' Return a 500 Internal Server Error response
                Return Request.CreateResponse(HttpStatusCode.InternalServerError, $"{ex.Message}")
            End Try
        End Function

    End Class
End Namespace