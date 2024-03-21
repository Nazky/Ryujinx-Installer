Imports System.Drawing.Drawing2D

Public Class RyujinxCustomTextBox
    Inherits TextBox

    Private _borderColor As Color = Color.Black
    Private _backgroundColor As Color = Color.White
    Private _cornerRadius As Integer = 10

    Public Property BorderColor As Color
        Get
            Return _borderColor
        End Get
        Set(value As Color)
            _borderColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Property BackgroundColor As Color
        Get
            Return _backgroundColor
        End Get
        Set(value As Color)
            _backgroundColor = value
            Me.Invalidate()
        End Set
    End Property

    Public Property CornerRadius As Integer
        Get
            Return _cornerRadius
        End Get
        Set(value As Integer)
            _cornerRadius = value
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim borderRect As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        Dim innerRect As New Rectangle(1, 1, Me.Width - 3, Me.Height - 3)

        Dim borderPath As New GraphicsPath()
        borderPath.AddArc(New Rectangle(0, 0, 2 * _cornerRadius, 2 * _cornerRadius), 180, 90)
        borderPath.AddArc(New Rectangle(Me.Width - 2 * _cornerRadius - 1, 0, 2 * _cornerRadius, 2 * _cornerRadius), -90, 90)
        borderPath.AddArc(New Rectangle(Me.Width - 2 * _cornerRadius - 1, Me.Height - 2 * _cornerRadius - 1, 2 * _cornerRadius, 2 * _cornerRadius), 0, 90)
        borderPath.AddArc(New Rectangle(0, Me.Height - 2 * _cornerRadius - 1, 2 * _cornerRadius, 2 * _cornerRadius), 90, 90)
        borderPath.CloseFigure()

        Dim borderPen As New Pen(_borderColor, 1)
        Dim backgroundBrush As New SolidBrush(_backgroundColor)

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        ' Draw background
        e.Graphics.FillPath(backgroundBrush, borderPath)

        ' Draw border
        e.Graphics.DrawPath(borderPen, borderPath)

        MyBase.OnPaint(e)
    End Sub
End Class
