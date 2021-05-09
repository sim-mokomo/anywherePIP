declare global {
	interface Window {
		api: SandBox
	}
}

export interface SandBox {
	SearchUrlRequest: (url: string) => void
	CloseWindowRequest: () => void
	MinimiseWindowRequest: () => void
	MaximiseWindowRequest: () => void
}
