Imports System.Drawing.Drawing2D

Public Class RyujinxProgressBar
    Inherits Control

    Private _value As Integer = 0
    Private _minimum As Integer = 0
    Private _maximum As Integer = 100

    Public Property Value As Integer
        Get
            Return _value
        End Get
        Set(value As Integer)
            If value < _minimum Then
                _value = _minimum
            ElseIf value > _maximum Then
                _value = _maximum
            Else
                _value = value
            End If
            Me.Invalidate()
        End Set
    End Property

    Public Property Minimum As Integer
        Get
            Return _minimum
        End Get
        Set(value As Integer)
            _minimum = value
            If _value < _minimum Then
                _value = _minimum
            End If
            Me.Invalidate()
        End Set
    End Property

    Public Property Maximum As Integer
        Get
            Return _maximum
        End Get
        Set(value As Integer)
            _maximum = value
            If _value > _maximum Then
                _value = _maximum
            End If
            Me.Invalidate()
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        Dim rectBorder As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        Dim fillWidth As Integer = CInt((_value - _minimum) / (_maximum - _minimum) * Me.Width)

        Dim graphicsPath As New GraphicsPath()
        graphicsPath.AddArc(New Rectangle(0, 0, Me.Height, Me.Height), 90, 180)
        graphicsPath.AddArc(New Rectangle(Me.Width - Me.Height - 1, 0, Me.Height, Me.Height), -90, 180)
        graphicsPath.CloseFigure()

        Dim backgroundBrush As New SolidBrush(Color.FromArgb(32, 32, 32))
        Dim progressBrush As New SolidBrush(Color.FromArgb(64, 200, 255))
        Dim borderPen As New Pen(Color.FromArgb(32, 32, 32), 0) ' Adjust thickness here
        borderPen.LineJoin = LineJoin.Round ' Make the corners of the border round

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias

        ' Fill background with ARGB (32, 32, 32) color
        e.Graphics.FillRectangle(backgroundBrush, Me.ClientRectangle)

        ' Create a rectangle that clips the progress to the rounded corner area
        Dim rectProgress As New Rectangle(0, 0, fillWidth, Me.Height - 1)
        Dim clipRegion As New Region(graphicsPath)
        e.Graphics.Clip = clipRegion

        ' Fill progress area with blue color within progress bar bounds
        e.Graphics.FillRectangle(progressBrush, rectProgress)

        ' Reset the clip region
        e.Graphics.ResetClip()

        ' Draw the rounded borders
        e.Graphics.DrawPath(borderPen, graphicsPath)
    End Sub
End Class
