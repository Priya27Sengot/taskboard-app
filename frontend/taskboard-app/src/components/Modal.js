export default function Modal({
    isOpen,
    title,
    onClose,
    children
}) {
    if (!isOpen) return null;

    return (
        <div className="modal-overlay">
            <div className="modal-content">

                <div className="modal-header">
                    <h3>{title}</h3>

                    <button
                        type="button"
                        onClick={onClose}
                    >
                        X
                    </button>
                </div>

                <div className="modal-body">
                    {children}
                </div>

            </div>
        </div>
    );
}