import Modal from "../components/Modal";

export default function Confirm({
    isOpen,
    title = "Confirm",
    message,
    onConfirm,
    onCancel
}) {
    return (
        <Modal isOpen={isOpen} onClose={onCancel}>
            <div className="confirm-box">
                <h3>{title}</h3>

                <p>{message}</p>

                <div className="confirm-actions">
                    <button
                        className="btn-secondary"
                        onClick={onCancel}
                    >
                        Cancel
                    </button>

                    <button
                        className="btn-danger"
                        onClick={onConfirm}
                    >
                        Confirm
                    </button>
                </div>
            </div>
        </Modal>
    );
}