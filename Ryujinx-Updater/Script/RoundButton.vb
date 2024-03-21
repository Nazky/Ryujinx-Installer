Imports System.Drawing.Drawing2D

Public Class RoundButton
    Inherits Button

    Private _cornerRadius As Integer = 5 ' Default corner radius

    Public Property CornerRadius As Integer
        Get
            Return _cornerRadius
        End Get
        Set(value As Integer)
            _cornerRadius = value
            Me.Invalidate() ' Redraw the button when the corner radius changes
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim graphicsPath As New GraphicsPath()
        Dim cornerRadiusRectangle As New Rectangle(0, 0, _cornerRadius * 2, _cornerRadius * 2)

        ' Top-left corner
        graphicsPath.AddArc(cornerRadiusRectangle, 180, 90)

        ' Top-right corner
        cornerRadiusRectangle.X = Me.Width - _cornerRadius * 2
        graphicsPath.AddArc(cornerRadiusRectangle, 270, 90)

        ' Bottom-right corner
        cornerRadiusRectangle.Y = Me.Height - _cornerRadius * 2
        graphicsPath.AddArc(cornerRadiusRectangle, 0, 90)

        ' Bottom-left corner
        cornerRadiusRectangle.X = 0
        graphicsPath.AddArc(cornerRadiusRectangle, 90, 90)

        graphicsPath.CloseFigure()
        Me.Region = New Region(graphicsPath)

        MyBase.OnPaint(e)
    End Sub
End Class
